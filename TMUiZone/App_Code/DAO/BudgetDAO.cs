using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Data;
using AshokaHRMBudget.DTO;

namespace AshokaHRMBudget.DAO
{
    public static class BudgetDAO
    {
        #region Budget
        public static DataTable BudgetForDdl(string Prefix)
        {
            try
            {
                string query = " select Name as BudgetId from " + Prefix + "$G_L Budget Name] with(nolock) order by BudgetId";
                return DBM.GetDataSet(query).Tables[0];
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public static SqlDataReader DepartmentForDdl(string Prefix)
        {
            try
            {
                //comd = DBM.GetCommand("select [Department Code] as DepartmentCode from " + Prefix + "$Department Master] with(nolock)");
                string query = "select Code as DepartmentCode from " + Prefix + "$Dimension Value] with(nolock) where upper([Dimension Code])='DEPARTMENT' order by DepartmentCode";
                return DBM.getReader(query);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public static SqlDataReader CampusForDdl(string Prefix)
        {
            try
            {
                string query = "select [Code] as CampusCode from " + Prefix + "$Dimension Value] with(nolock) where upper([Dimension Code])='CAMPUS' order by CampusCode";
                return DBM.getReader(query);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public static DataTable DepartmentForDdlByEmployee(string Prefix, string EmployeeId)
        {
            try
            {
                //string query = "select distinct [Department Code] as DepartmentCode from " + Prefix + "$Employee] with(nolock) where No_ = '" + EmployeeId + "' or HOD = '" + EmployeeId + "' order by DepartmentCode";
                //string query = "select [Department Code] as DepartmentCode  from " + Prefix + "$Department Master] with(nolock) where [HOD-1] = '" + EmployeeId + "' or [HOD-2] = '" + EmployeeId + "'";
                //string query = " select  [Department Code] as DepartmentCode into #Department from " + Prefix + "$Employee] with(nolock) where No_ = '" + EmployeeId + "' " +
                //               " insert into #Department select [Department Code] from " + Prefix + "$Department Master] with(nolock) where [HOD-1] = '" + EmployeeId + "'"+// or [HOD-2] = '" + EmployeeId + "' " +
                //               " select distinct DepartmentCode from #Department drop table #Department "; 
                string query =  " select [Department Code] as DepartmentCode into #Department from " + Prefix + "$Employee] with(nolock) where No_ = '" + EmployeeId + "'" +
                                " insert into #Department select [Department Code] from " + Prefix + "$Department Master] with(nolock) where [HOD-1] = '" + EmployeeId + "'" +
                                " select No_ as EmpId into #RMEmployeeList from " + Prefix + "$Employee] with(nolock) where HOD = '" + EmployeeId + "'" +
                                " while exists (select * from #RMEmployeeList) begin " +
                                " insert into #Department select [Department Code] from " + Prefix + "$Employee] with(nolock) where No_ in (select EmpId from #RMEmployeeList) " +
                                " insert into #Department select [Department Code] from " + Prefix + "$Department Master] with(nolock) where [HOD-1] in (select EmpId from #RMEmployeeList) " +
                                " select * into #tempRMEmployeeList from #RMEmployeeList " +
                                " delete from #RMEmployeeList " +
                                " insert into #RMEmployeeList select No_ from " + Prefix + "$Employee] with(nolock) where HOD in (select EmpId from #RMEmployeeList) " +
                                " drop table #tempRMEmployeeList end " +
                                " drop table #RMEmployeeList " +
                                " select distinct DepartmentCode from #Department where DepartmentCode != '' drop table #Department";
                
                return DBM.GetDataSet(query).Tables[0];
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public static void EmployeeBudgetPermission(string Prefix, string EmployeeId, out Int32 BudgetEntry, out Int32 BudgetReview, out Int32 BudgetApprove)
        {
            SqlDataReader rdr = null;
            try
            {
                BudgetEntry = BudgetReview = BudgetApprove = 0;
                string query = "select [Budget Entry] as BudgetEntry, [Budget Review] as BudgetReview, [Budget Approve] as BudgetApprove from " + Prefix + "$Employee] with(nolock) where No_ = '" + EmployeeId + "'";
                rdr = DBM.getReader(query);
                if (rdr.HasRows)
                {
                    while (rdr.Read())
                    {
                        BudgetEntry = Convert.ToInt32(rdr["BudgetEntry"]);
                        BudgetReview = Convert.ToInt32(rdr["BudgetReview"]);
                        BudgetApprove = Convert.ToInt32(rdr["BudgetApprove"]);
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                if (rdr != null)
                {
                    if (!rdr.IsClosed) { rdr.Close(); }
                    rdr = null;
                }
            }
        }

        public static DataSet GLAccountByBudgetName(string NavDbName, string Company, string DepartmentCode, string CampusCode, string BudgetId, string BudgetName, out string EmployeeId, out string BudgetStatus, out string Remark, out string ReviewedBy, out string ApprovedBy, string GLRange)
        {
            SqlCommand comd = null;
            EmployeeId = BudgetStatus = Remark = ReviewedBy = ApprovedBy = "";
            try
            {
                // CreateTable
                comd = DBM.GetCommandSP("CreateBudgetTable");
                comd.Parameters.AddWithValue("@Company", Company);
                DBM.WriteToDb(comd);
                
                // Insert GL In Table
                string query1 = "";
                string query = "insert into [" + Company + "$Budget] " +
                                    "([GL Code],[GL Name],[Department],[Campus Code],[Budget ID],[Budget Name],[Status Date]) " +
                            "select No_, Name, '" + DepartmentCode + "', '" + CampusCode + "', '" + BudgetId + "','" + BudgetName + "', getdate()  from " + NavDbName + "[" + Company + "$G_L Account] as a with(nolock) " +
                            "where a.No_ between "+GLRange+" and [Account Type] = 0 and a.No_ not in (select [GL Code] from [" + Company + "$Budget] as b with(nolock) where b.Department = '" + DepartmentCode + "' and [Campus Code] = '" + CampusCode + "' and [Budget ID] = '" + BudgetId + "' ) ";

                // Update Status Of GL Blocked or Not From Nav
                query = query + " update a set a.[Blocked] = b.[Blocked] "+
                                " from [" + Company + "$Budget] as a inner join " + NavDbName + "[" + Company + "$G_L Account] as b with(nolock) on a.[GL Code] = b.No_ ";

                // Update Head Of Each GL
                query = query + " Select No_ as HeadCode,REPLACE(Totaling,'..',' and ') As Range from " + NavDbName + "[" + Company + "$G_L Account] where No_ between " + GLRange + "  and [Account Type] = 4";

                foreach (DataRow dr in DBM.GetDataSet(query).Tables[0].Rows)
                {
                    query1 = query1 + " update [" + Company + "$Budget] set [Head Code] = '" + dr["HeadCode"] + "' where [GL Code] between " + dr["Range"];
                }

                DBM.WriteToDb(query1);

                // Get Data Head Wise
                using (DataSet ds = GLHead(NavDbName, Company, DepartmentCode, CampusCode, BudgetId))
                {
                    EmployeeId = ds.Tables[1].Rows[0]["EmployeeId"].ToString();
                    BudgetStatus = ds.Tables[1].Rows[0]["Status"].ToString();
                    Remark = ds.Tables[1].Rows[0]["Remark"].ToString();
                    ReviewedBy = ds.Tables[1].Rows[0]["ReviewedBy"].ToString();
                    ApprovedBy = ds.Tables[1].Rows[0]["ApprovedBy"].ToString();
                    return ds;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                if (comd != null)
                {
                    comd.Dispose();
                    comd = null;
                }
            }
        }
        public static DataSet GLHead(string NavDbName, string Company, string DepartmentCode, string CampusCode, string BudgetId)
        {
            try
            {
                string[] year = BudgetId.Split('-');
                string PreviousFinancialStartDate = "04/01/" + (Convert.ToInt32(year[0].Trim()) - 1).ToString();

                string query = "Declare @PreviousBudget varchar(20) select @PreviousBudget = max(Name) from " + NavDbName + "[" + Company + "$G_L Budget Name] with(nolock) where Name < '" + BudgetId + "' " +
                                    " select isnull(sum(isnull(a.Amount,0)),0) as PYB,a.[G_L Account No_] " +
                                    " into #tempTotalBudget " +
                                    " from " + NavDbName + "[" + Company + "$G_L Budget Entry] as a with(nolock) " +
                                    " group by a.[G_L Account No_],a.[Budget Name],a.[Global Dimension 1 Code], a.[Global Dimension 2 Code],a.[Date] " +
                                    " having a.[Budget Name] = @PreviousBudget and a.[Global Dimension 1 Code] = '" + CampusCode + "' and a.[Global Dimension 2 Code] = '" + DepartmentCode + "'"+
                                    " and cast(Convert(varchar(10),a.Date,101) as date) between " +
                                                                                " cast('" + PreviousFinancialStartDate + "' as date) and cast('03/31/"+year[0]+"' as date)";

                query = query + " Select isnull(sum(isnull(a.Amount,0)),0) as YTDB,a.[G_L Account No_] " +
                                " into #tempYTDBudget " +
                                " from " + NavDbName + "[" + Company + "$G_L Budget Entry] as a with(nolock) " +
                                " group by a.[G_L Account No_],a.[Budget Name],a.[Global Dimension 1 Code], a.[Global Dimension 2 Code],a.[Date] " +
                                " having a.[Budget Name] = @PreviousBudget and a.[Global Dimension 1 Code] = '" + CampusCode + "' and a.[Global Dimension 2 Code] = '" + DepartmentCode + "' " +
                                " and cast(Convert(varchar(10),a.Date,101) as date) between " +
                                                  " cast('" + PreviousFinancialStartDate + "' as date) and cast(CONVERT(varchar(10),DATEADD(s,-1,DATEADD(mm, DATEDIFF(m,0,cast(cast(Datepart(m,getdate()) as varchar)+'/01/'+ case when (Datepart(m,getdate()) > 3) then '" + (Convert.ToInt32(year[0]) - 1).ToString() + "' else '" + year[0] + "' end as date)),0)),101) as date)";

                query = query + " select isnull(sum(isnull(a.Amount,0)),0) as YTDA,a.[G_L Account No_] " +
                                " into #tempYTDActuals " +
                                " from " + NavDbName + "[" + Company + "$G_L Entry] as a with(nolock) " +
                                " group by a.[G_L Account No_] ,a.[Global Dimension 1 Code], a.[Global Dimension 2 Code],a.[Posting Date] " +
                                " having a.[Global Dimension 1 Code] = '" + CampusCode + "' and a.[Global Dimension 2 Code] = '" + DepartmentCode + "' " +
                                " and cast(Convert(varchar(10),a.[Posting Date],101) as date) between " +
                                                        " cast('" + PreviousFinancialStartDate + "' as date) and cast(CONVERT(varchar(10),DATEADD(s,-1,DATEADD(mm, DATEDIFF(m,0,cast(cast(Datepart(m,getdate()) as varchar)+'/01/'+ case when (Datepart(m,getdate()) > 3) then '" + (Convert.ToInt32(year[0]) - 1).ToString() + "' else '" + year[0] + "' end as date)),0)),101) as date)";

                query = query + " select isnull(sum(isnull(PYB,0)),0) as PYB, [G_L Account No_] into #TotalBudget from #tempTotalBudget group by [G_L Account No_] " +
                                " select isnull(sum(isnull(YTDB,0)),0) as YTDB,[G_L Account No_] into #YTDBudget from #tempYTDBudget group by [G_L Account No_] " +
                                " select isnull(sum(isnull(YTDA,0)),0) as YTDA,[G_L Account No_] into #YTDActuals from #tempYTDActuals group by [G_L Account No_] " +
                                " drop table #tempTotalBudget drop table #tempYTDBudget drop table #tempYTDActuals ";

                query = query + " select Replace(Totaling,'..',' and ') as GLRange,a.[Head Code] as Code,b.Name, cast(isnull(sum(isnull(c.PYB,0)),0) as decimal(18,0)) as PYB, cast(isnull(sum(isnull(d.YTDB,0)),0) as decimal(18,0)) as YTDB, cast(isnull(sum(isnull(e.YTDA,0)),0) as decimal(18,0)) as YTDA, " +
                                " isnull(sum(isnull(a.[Budget Amount],0)),0) as BudgetedAmount,isnull(sum(isnull(a.April,0)),0) as April,isnull(sum(isnull(a.May,0)),0) as May,isnull(sum(isnull(a.June,0)),0) as June, " +
                                " isnull(sum(isnull(a.July,0)),0) as July,isnull(sum(isnull(a.August,0)),0) as August,isnull(sum(isnull(a.Sept,0)),0) as Sept,isnull(sum(isnull(a.Oct,0)),0) as Oct, " +
                                " isnull(sum(isnull(a.Nov,0)),0) as Nov,isnull(sum(isnull(a.Dec,0)),0) as Dec,isnull(sum(isnull(a.Jan,0)),0) as Jan,isnull(sum(isnull(a.Feb,0)),0) as Feb, " +
                                " isnull(sum(isnull(a.March,0)),0) as March " +
                                " from [" + Company + "$Budget] as a with(nolock) " +
                                " left join #TotalBudget as c on a.[GL Code] = c.[G_L Account No_]  " +
                                " left join #YTDBudget as d on a.[GL Code] = d.[G_L Account No_] " +
                                " left join #YTDActuals as e on a.[GL Code] = e.[G_L Account No_] " +
                                " inner join " + NavDbName + "[" + Company + "$G_L Account] as b with(nolock) on a.[Head Code] = b.No_ " +
                                " group by a.[head code],b.Name,[Budget ID],Department,[Campus Code],Totaling,a.Blocked " +
                                " having [Budget ID] = '" + BudgetId + "' and Department = '" + DepartmentCode + "' and [Campus Code] = '" + CampusCode + "' and a.[Head Code] is not null and a.Blocked = 0 " +

                                " drop table #TotalBudget  drop table #YTDActuals  drop table #YTDBudget " +

                                " Select top 1 isnull([Employee ID],'') as EmployeeId, isnull([Reviewed By],'') as ReviewedBy, isnull([Approved By],'') as ApprovedBy, isnull([Status],'') as Status, isnull(Remarks,'') as Remark FROM  [dbo].[" + @Company + "$Budget] with(nolock) " +
                                " where [Budget ID] = '" + BudgetId + "' and Department = '" + DepartmentCode + "' and [Campus Code] = '" + CampusCode + "' ";

                return (DBM.GetDataSet(query));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
                
            }
        }
        public static DataSet GLAccountByGLRange(string NavDbName, string Company, string DepartmentCode, string CampusCode, string BudgetId, string GLRange)
        {
            try
            {
                string[] year = BudgetId.Split('-');
                string PreviousFinancialStartDate = "04/01/" + (Convert.ToInt32(year[0].Trim()) - 1).ToString();

                string query = "Declare @PreviousBudget varchar(20) select @PreviousBudget = max(Name) from " + NavDbName + "[" + Company + "$G_L Budget Name] with(nolock) where Name < '" + BudgetId + "' " +
                                    " select isnull(sum(isnull(a.Amount,0)),0) as PYB,a.[G_L Account No_] " +
                                    " into #tempTotalBudget " +
                                    " from " + NavDbName + "[" + Company + "$G_L Budget Entry] as a with(nolock) " +
                                    " group by a.[G_L Account No_],a.[Budget Name],a.[Global Dimension 1 Code], a.[Global Dimension 2 Code],a.[Date] " +
                                    " having a.[Budget Name] = @PreviousBudget and a.[Global Dimension 1 Code] = '" + CampusCode + "' and a.[Global Dimension 2 Code] = '" + DepartmentCode + "'"+
                                    " and cast(Convert(varchar(10),a.Date,101) as date) between " +
                                                                                " cast('" + PreviousFinancialStartDate + "' as date) and cast('03/31/"+year[0]+"' as date) "+
                                    "  and [G_L Account No_] between " + GLRange + "";

                query = query + " Select isnull(sum(isnull(a.Amount,0)),0) as YTDB,a.[G_L Account No_] " +
                                " into #tempYTDBudget " +
                                " from " + NavDbName + "[" + Company + "$G_L Budget Entry] as a with(nolock) " +
                                " group by a.[G_L Account No_],a.[Budget Name],a.[Global Dimension 1 Code], a.[Global Dimension 2 Code],a.[Date] " +
                                " having a.[Budget Name] = @PreviousBudget and a.[Global Dimension 1 Code] = '" + CampusCode + "' and a.[Global Dimension 2 Code] = '" + DepartmentCode + "' " +
                                " and cast(Convert(varchar(10),a.Date,101) as date) between " +
                                                  " cast('" + PreviousFinancialStartDate + "' as date) and cast(CONVERT(varchar(10),DATEADD(s,-1,DATEADD(mm, DATEDIFF(m,0,cast(cast(Datepart(m,getdate()) as varchar)+'/01/'+ case when (Datepart(m,getdate()) > 3) then '" + (Convert.ToInt32(year[0]) - 1).ToString() + "' else '" + year[0] + "' end as date)),0)),101) as date) " +
                                " and [G_L Account No_] between " + GLRange + "";

                query = query + " select isnull(sum(isnull(a.Amount,0)),0) as YTDA,a.[G_L Account No_] " +
                                " into #tempYTDActuals " +
                                " from " + NavDbName + "[" + Company + "$G_L Entry] as a with(nolock) " +
                                " group by a.[G_L Account No_] ,a.[Global Dimension 1 Code], a.[Global Dimension 2 Code],a.[Posting Date] " +
                                " having a.[Global Dimension 1 Code] = '" + CampusCode + "' and a.[Global Dimension 2 Code] = '" + DepartmentCode + "' " +
                                " and cast(Convert(varchar(10),a.[Posting Date],101) as date) between " +
                                                        " cast('" + PreviousFinancialStartDate + "' as date) and cast(CONVERT(varchar(10),DATEADD(s,-1,DATEADD(mm, DATEDIFF(m,0,cast(cast(Datepart(m,getdate()) as varchar)+'/01/'+ case when (Datepart(m,getdate()) > 3) then '" + (Convert.ToInt32(year[0]) - 1).ToString() + "' else '" + year[0] + "' end as date)),0)),101) as date) "+
                                "  and [G_L Account No_] between " + GLRange + "";

                query = query + " select isnull(sum(isnull(PYB,0)),0) as PYB, [G_L Account No_] into #TotalBudget from #tempTotalBudget group by [G_L Account No_] " +
                                " select isnull(sum(isnull(YTDB,0)),0) as YTDB,[G_L Account No_] into #YTDBudget from #tempYTDBudget group by [G_L Account No_] " +
                                " select isnull(sum(isnull(YTDA,0)),0) as YTDA,[G_L Account No_] into #YTDActuals from #tempYTDActuals group by [G_L Account No_] " +
                                " drop table #tempTotalBudget drop table #tempYTDBudget drop table #tempYTDActuals ";

                query = query + " SELECT [id] as ID, isnull([GL Code],'') as Code, isnull([GL Name],'') as Name,cast(isnull(c.PYB,0) as decimal(18,0)) as PYB, cast(isnull(d.YTDB,0) as decimal(18,0)) as YTDB, cast(isnull(e.YTDA,0) as decimal(18,0)) as YTDA, " +
                                    " isnull([Budget Amount],0) as BudgetedAmount, " +
                                    " isnull([Jan],0) as Jan, isnull([Feb],0) as Feb, isnull([March],0) as March, isnull([April],0) as April, isnull([May],0) as May, isnull([June],0) as June, " +
                                    " isnull([July],0) as July, isnull([August],0) as  Aug,isnull([Sept],0) as sept, isnull([Oct],0) as Oct, isnull([Nov],0) Nov, isnull([Dec],0) as Dec " +
                                    " FROM  [" + @Company + "$Budget] as a with(nolock) "+
                                    " left join #TotalBudget as c on a.[GL Code] = c.[G_L Account No_]  " +
                                    " left join #YTDBudget as d on a.[GL Code] = d.[G_L Account No_] " +
                                    " left join #YTDActuals as e on a.[GL Code] = e.[G_L Account No_] " +
                                    " where [Budget ID] = '" + BudgetId + "' and Department = '" + DepartmentCode + "' and [Campus Code] = '" + CampusCode + "' and [GL Code] between "+GLRange+" and Blocked = 0 " +
                                    " order by isnull([GL Name],'') "+
                                    " drop table #TotalBudget  drop table #YTDActuals  drop table #YTDBudget ";
                return DBM.GetDataSet(query);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);   
            }
        }
        
        public static void UpdateBudgetDetails(BudgetDTO objDTO, string EmployeeId)
        {
            try
            {
                string query = "update ["+objDTO.CompanyName+ "$Budget]"+
				                        "set Jan = '" +objDTO.Jan.ToString()+ "', Feb = '" +objDTO.Feb.ToString()+ "', March = '" +objDTO.March.ToString()+ "',"+
                                        "April = '" +objDTO.April.ToString()+ "', May = '" +objDTO.May.ToString()+ "', June = '" +objDTO.June.ToString()+ "',"+
                                        "July = '" +objDTO.July.ToString()+ "', August = '" +objDTO.Aug.ToString()+ "', Sept = '" +objDTO.Sept.ToString()+ "',"+
                                        "Oct = '" +objDTO.Oct.ToString()+ "', Nov = '" +objDTO.Nov.ToString()+ "', Dec = '" +objDTO.Dec.ToString()+ "',"+ 
                                        "[Budget Amount] = '" +objDTO.BudgetedAmount.ToString()+ "', [Edit Employee ID] = '" + EmployeeId +"'"+
		                            "where id = '" +objDTO.Id+ "'";
                DBM.WriteToDb(query);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public static void BudgetSendForReview(string CompanyName, string BudgetId, string DepartmentCode, string CampusCode, string EmployeeId)
        {
            try
            {
                string query = "update [" + CompanyName + "$Budget] set [Employee ID] = '" + EmployeeId + "', [Created Date] =  GETDATE(), [Status] = 'PROCESSING', [Status Date] = getdate()" +
	                                "where [Budget ID] = '" + BudgetId + "' and Department = '" + DepartmentCode + "' and [Campus Code] = '" + CampusCode + "' and Blocked = 0";
                DBM.WriteToDb(query);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public static void BudgetSendForApproval(string CompanyName, string BudgetId, string DepartmentCode, string CampusCode, string Remark, string EmployeeId)
        {
            try
            {
                string query = "update [" + CompanyName + "$Budget] set Remarks = '" + Remark + "', [Status Date] = GETDATE(), " +
                                                " [Reviewed By] = '" + EmployeeId + "', [Reviewed Date] =  GETDATE(), [Status] = 'REVIEWED' " +
                                " where [Budget ID] = '" + BudgetId + "' and Department = '" + DepartmentCode + "' and [Campus Code] = '" + CampusCode + "'  and Blocked = 0";
                DBM.WriteToDb(query);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public static void BudgetApprove(string NavDbName, string CompanyName, string BudgetId, string DepartmentCode, string CampusCode, string Remark, string EmployeeId)
        {
            try
            {
                string[] year = BudgetId.Split('-');
                //BudgetId = year[0].Trim() + " - " + year[1].Trim();

                string Query = "";
                Query = "select isnull([GL Code],'') as GLCode,isnull(April,0) as April,isnull(May,0) as May,isnull(June,0) as June, isnull(July,0) as July, " +
                                " isnull(August,0) as August,isnull(Sept,0) as Sept,isnull(Oct,0) as Oct,isnull(Nov,0) as Nov, isnull(Dec, 0) as Dec, isnull(Jan,0) as Jan, " +
                                " isnull(feb,0) as feb,isnull(March,0) as March " +
                        " from [" + CompanyName + "$Budget] with(nolock) " +
                        " where [Budget ID] = '" + BudgetId + "' and Department = '" + DepartmentCode + "' and [Campus Code] = '" + CampusCode + "' and Upper([Status]) != 'APPROVED'  and Blocked = 0" +

                        " Declare @EntryNo int " +
                        " select @EntryNo = isnull(max([Entry No_]),0) from " + NavDbName + "[" + CompanyName + "$G_L Budget Entry] " +
                        " set @EntryNo = isnull(@EntryNo,0) " +

                        " Declare @DimensionSetID int " +
                        " select @DimensionSetID = a.[Dimension Set ID] from " + NavDbName + "[" + CompanyName + "$Dimension Set Entry] as a with(nolock) " +
                        " inner join " + NavDbName + "[" + CompanyName + "$Dimension Set Entry] as c on a.[Dimension Set ID] = c.[Dimension Set ID] " +
                        " where a.[Dimension Set ID] not in (select [Dimension Set ID] from " + NavDbName + "[" + CompanyName + "$Dimension Set Entry] as b where b.[Dimension Code] not in ('CAMPUS','DEPARTMENT')) " +
                        " and a.[Dimension Code] = 'CAMPUS' and a.[Dimension Value Code] = '" + CampusCode + "' and c.[Dimension Code] = 'DEPARTMENT' and c.[Dimension Value Code] = '" + DepartmentCode + "' " +
                        " set @DimensionSetID = isnull(@DimensionSetID,0) " +
                        " select @EntryNo as EntryNo, @DimensionSetID as DimensionSetID";

                    using (DataSet ds = DBM.GetDataSet(Query))
                    {
                        Query = "";
                        string GLACCOUNT, Date;
                        
                        Int32 EntryNo = Convert.ToInt32(ds.Tables[1].Rows[0]["EntryNo"]);
                        Int32 DimensionSetID = Convert.ToInt32(ds.Tables[1].Rows[0]["DimensionSetID"]);
                        foreach (DataRow dr in ds.Tables[0].Rows)
                        {
                            GLACCOUNT = dr["GLCode"].ToString();
                            foreach (DataColumn dc in ds.Tables[0].Columns)
                            {
                                if (dc.ColumnName == "GLCode")
                                {
                                    continue;
                                }
                                EntryNo++; //Incresed here Coz There Will Be Only One User In Whole Organization Who Will Approve Budget.
                                Date = dc.ColumnName == "April" ? "04/01/" + year[0].Trim() : dc.ColumnName == "May" ? "05/01/" + year[0].Trim() :
                                       dc.ColumnName == "June" ? "06/01/" + year[0].Trim() : dc.ColumnName == "July" ? "07/01/" + year[0].Trim() :
                                       dc.ColumnName == "August" ? "08/01/" + year[0].Trim() : dc.ColumnName == "Sept" ? "09/01/" + year[0].Trim() :
                                       dc.ColumnName == "Oct" ? "10/01/" + year[0].Trim() : dc.ColumnName == "Nov" ? "11/01/" + year[0].Trim() :
                                       dc.ColumnName == "Dec" ? "12/01/" + year[0].Trim() : dc.ColumnName == "Jan" ? "01/01/" + year[1].Trim() :
                                       dc.ColumnName == "feb" ? "02/01/" + year[1].Trim() : "03/01/" + year[1].Trim();

                                Query = Query + " insert into " + NavDbName + "[" + CompanyName + "$G_L Budget Entry] " +
                                                            "([Entry No_],[Budget Name],[G_L Account No_],[Date],[Global Dimension 1 Code],[Global Dimension 2 Code],[Amount],[Description],[Business Unit Code],[User ID],[Budget Dimension 1 Code],[Budget Dimension 2 Code],[Budget Dimension 3 Code],[Budget Dimension 4 Code],[Last Date Modified],[Dimension Set ID],[Forecast Amount])" +
                                                " values ( " + EntryNo + ", '" + BudgetId + "','" + GLACCOUNT + "', cast('" + Date + "'as date), '" + CampusCode + "','" + DepartmentCode + "','" + dr[dc].ToString() + "','','','" + EmployeeId + "','','','','',cast(getdate() as date),'" + DimensionSetID + "',0) ";
                            }

                        }
                    }
                    Query = Query + " update [" + CompanyName + "$Budget] set Remarks = '" + Remark + "', [Status Date] =  GETDATE(), [Status] = 'APPROVED', [Approved By] = '" + EmployeeId + "',  [Approved Date] = getdate()" +
                                   " where [Budget ID] = '" + BudgetId + "' and Department = '" + DepartmentCode + "' and [Campus Code] = '" + CampusCode + "'  and Blocked = 0";

                DBM.WriteToDbWithTransaction(Query);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public static void BudgetReject(string CompanyName, string BudgetId, string DepartmentCode, string CampusCode, string Remark, string EmployeeId)
        {
            try
            {
                string Query = " update [" + CompanyName + "$Budget] set Remarks = '" + Remark + "', [Status Date] = GETDATE(), " +
                                        " [Reviewed By] = case(upper([Status])) when 'PROCESSING' then '" + EmployeeId + "' else [Reviewed By] end, " +
                                        " [Reviewed Date] = case(upper([Status])) when 'PROCESSING' then getdate() else [Reviewed Date] end, "+
                                        " [Approved By] = case(upper([Status])) when 'REVIEWED' then '" + EmployeeId + "' else NULL end, " +
                                        " [Approved Date] = case(upper([Status])) when 'REVIEWED' then getdate() else NULL end, " +
                                        " [Status] = 'SENTBACK'" +
                                   " where [Budget ID] = '" + BudgetId + "' and Department = '" + DepartmentCode + "' and [Campus Code] = '" + CampusCode + "'  and Blocked = 0";

                DBM.WriteToDbWithTransaction(Query);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public static string GetToMailIds(string Prefix, string Company, string BudgetId, string Department, string CampusCode, string BudgetStatus, string BudgetScreen)
        {
            try
            {
                string Query = "";
                //if (BudgetStatus == "PROCESSING" || BudgetStatus == "REVIEWED")
                //{
                //    Query = " select distinct [Company E-Mail] as Id from " + Prefix + "$Employee] as a with(nolock) "+
                //            " where (a.No_ in (select b.No_ from " + Prefix + "$Employee] as b with(nolock) where b.[Department Code] = '" + Department + "') or a.No_ in (select b.HOD from " + Prefix + "$Employee] as b with(nolock) where b.[Department Code] = '" + Department + "')) " +
                //            " and " + (BudgetStatus == "PROCESSING" ? "[Budget Review] = 1" : " [Budget Approve] = 1") + "";
                //}
                if (BudgetStatus == "PROCESSING" || BudgetStatus == "REVIEWED")
                {
                    Query = " select distinct [Company E-Mail] as Id from " + Prefix + "$Employee] as a with(nolock) "+
                            " where (a.No_ in (select [HOD-1] from " + Prefix + "$Department Master] as b with(nolock) where b.[Department Code] = '" + Department + "') or a.No_ in (select [HOD-2] from " + Prefix + "$Department Master] as b with(nolock) where b.[Department Code] = '" + Department + "')) " +
                            " and " + (BudgetStatus == "PROCESSING" ? "[Budget Review] = 1" : " [Budget Approve] = 1") + "";
                }
                else if (BudgetStatus == "APPROVED")
                {
                    Query = " Declare @EmployeeId varchar(50), @ReviewedId varchar(50)   select @EmployeeId = [Employee ID],@ReviewedId = [Reviewed By]  from [" + Company + "$Budget] with(nolock) where [Budget ID] = '" + BudgetId + "' and [Campus Code] = '" + CampusCode + "' and Department = '" + Department + "' " +
                            " select [Company E-Mail] as Id from " + Prefix + "$Employee] with(nolock) where No_ in (@EmployeeId,@ReviewedId)";
                }
                else if (BudgetStatus == "SENTBACK")
                {
                    Query = "select [Company E-Mail] as Id from " + Prefix + "$Employee] with(nolock) where No_ in (select " + (BudgetScreen == "REVIEW"? " [Employee ID]" : "[Reviewed By]")+" from [" + Company + "$Budget] with(nolock) where [Budget ID] = '" + BudgetId + "' and [Campus Code] = '" + CampusCode + "' and Department = '" + Department + "') ";
                }
                
                string ToId = "";
                using(SqlDataReader rdr = DBM.getReader(Query))
                {
                    if (rdr.HasRows)
                    {
                        while (rdr.Read())
                        {
                            ToId += rdr["Id"].ToString()+",";
                        }
                    }
                }
                return ToId;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        #endregion

        #region BudgetReport
        public static DataTable GLForDdl(string Prefix, string GLRange)
        {
            try
            {
                string query = "select No_ as Id, Name as DepartmentCode from " + Prefix + "$G_L Account] as a with(nolock) where a.No_ between " + GLRange + " and [Account Type] = 0  and Blocked = 0 order By DepartmentCode";
                return DBM.GetDataSet(query).Tables[0];
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public static DataTable rptBudgetByDepartment(string NavDbName, string Company, string BudgetId, string DepartmentCode, string CampusCode, bool DepartmentWise, bool QuaterWise, string EmployeeId, string EmployeeDepartments)
        {
            try
            {
                string[] year = BudgetId.Split('-');
                string PreviousFinancialStartDate = "04/01/" + (Convert.ToInt32(year[0].Trim()) - 1).ToString();
                // Previous Year details
                string query = "IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[" + Company + "$Budget]') AND type in (N'U')) begin" +
                                    " Declare @PreviousBudget varchar(20) select @PreviousBudget = max(Name) from " + NavDbName + "[" + Company + "$G_L Budget Name] with(nolock) where Name < '" + BudgetId + "' " +
                                    " select [G_L Account No_],[Global Dimension 2 Code],isnull(Amount,0) as PYB " +
                                    " into #tempTotalBudget " +
                                    " from " + NavDbName + "[" + Company + "$G_L Budget Entry] with(nolock) " +
                                    " Where [Budget Name] = @PreviousBudget " + (CampusCode == "0" ? "" : " and [Global Dimension 1 Code] = '" + CampusCode + "'") + " and " + (DepartmentWise ? " [Global Dimension 2 Code] " : " [G_L Account No_] ") + " in (select * from dbo.split('" + DepartmentCode + "')) " +
                                    " and cast(Convert(varchar(10),[Date],101) as date) between " +
                                                                                " cast('" + PreviousFinancialStartDate + "' as date) and cast('03/31/" + year[0] + "' as date) ";

                query = query + " Select [G_L Account No_],[Global Dimension 2 Code],isnull(Amount,0) as YTDB " +
                                " into #tempYTDBudget " +
                                " from " + NavDbName + "[" + Company + "$G_L Budget Entry] with(nolock) " +
                                " where [Budget Name] = @PreviousBudget " + (CampusCode == "0" ? "" : " and [Global Dimension 1 Code] = '" + CampusCode + "'") + " and " + (DepartmentWise ? " [Global Dimension 2 Code] " : " [G_L Account No_] ") + " in (select * from dbo.split('" + DepartmentCode + "')) " +
                                " and cast(Convert(varchar(10),[Date],101) as date) between " +
                                                  " cast('" + PreviousFinancialStartDate + "' as date) and cast(CONVERT(varchar(10),DATEADD(s,-1,DATEADD(mm, DATEDIFF(m,0,cast(cast(Datepart(m,getdate()) as varchar)+'/01/'+ case when (Datepart(m,getdate()) > 3) then '" + (Convert.ToInt32(year[0]) - 1).ToString() + "' else '" + year[0] + "' end as date)),0)),101) as date) ";

                query = query + " select [G_L Account No_],[Global Dimension 2 Code],isnull(Amount,0) as YTDA " +
                                " into #tempYTDActuals " +
                                " from " + NavDbName + "[" + Company + "$G_L Entry] with(nolock) " +
                                " where " + (DepartmentWise ? " [Global Dimension 2 Code] " : " [G_L Account No_] ") + " in (select * from dbo.split('" + DepartmentCode + "')) " + (CampusCode == "0" ? "" : " and [Global Dimension 1 Code] = '" + CampusCode + "'") +
                                " and cast(Convert(varchar(10),[Posting Date],101) as date) between " +
                                                        " cast('" + PreviousFinancialStartDate + "' as date) and cast(CONVERT(varchar(10),DATEADD(s,-1,DATEADD(mm, DATEDIFF(m,0,cast(cast(Datepart(m,getdate()) as varchar)+'/01/'+ case when (Datepart(m,getdate()) > 3) then '" + (Convert.ToInt32(year[0]) - 1).ToString() + "' else '" + year[0] + "' end as date)),0)),101) as date) ";
                // Current Year Details
                query = query + " SELECT isnull([GL Code],'') as Code, isnull([GL Name],'') as Name, isnull(Department,'') as Department, "+
                                            " isnull([Budget Amount],0) as BudgetedAmount, isnull([April],0) as April, isnull([May],0) as May, isnull([June],0) as June, " +
                                            " isnull([July],0) as July, isnull([August],0) as  August, isnull([Sept],0) as September, " +
                                            " isnull([Oct],0) as October, isnull([Nov],0) November, isnull([Dec],0) as December, " +
                                            " isnull([Jan],0) as January, isnull([Feb],0) as February, isnull([March],0) as March, Status " +
                                    " into #tempBudget" +
                                    " FROM  [" + Company + "$Budget] with(nolock) " +
                                    " where [Budget ID] = '" + @BudgetId + "'" + (CampusCode == "0" ? "" : " and [Campus Code] = '" + CampusCode + "'") + " and " + (DepartmentWise ? " Department " : " [GL Code] ") + " in (select * from dbo.split('" + DepartmentCode + "')) "+(DepartmentWise ? "" : " and Department in (select * from dbo.Split('"+EmployeeDepartments+"')) ") +" and Blocked = 0 ";

                // Grouping By either [G_L Account No_] or Department
                query = query + " select isnull(sum(isnull(PYB,0)),0) as PYB," + (DepartmentWise ? " [G_L Account No_] " : " [Global Dimension 2 Code] as Department ") + " into #TotalBudget from #tempTotalBudget group by " + (DepartmentWise ? " [G_L Account No_] " : " [Global Dimension 2 Code] ") +
                                " select isnull(sum(isnull(YTDB,0)),0) as YTDB," + (DepartmentWise ? " [G_L Account No_] " : " [Global Dimension 2 Code] as Department ") + " into #YTDBudget from #tempYTDBudget group by " + (DepartmentWise ? " [G_L Account No_] " : " [Global Dimension 2 Code] ") +
                                " select isnull(sum(isnull(YTDA,0)),0) as YTDA," + (DepartmentWise ? " [G_L Account No_] " : " [Global Dimension 2 Code] as Department ") + " into #YTDActuals from #tempYTDActuals group by " + (DepartmentWise ? " [G_L Account No_] " : " [Global Dimension 2 Code] ") +

                                " SELECT " + (DepartmentWise ? " isnull(Code,'') as Code, isnull(Name,'') as Name, " : " isnull(Department,'') as Department, ") + " isnull(sum(isnull(BudgetedAmount,0)),0) as BudgetedAmount, " +
                                            " isnull(sum(isnull(April,0)),0) as April, isnull(sum(isnull(May,0)),0) as May, isnull(sum(isnull(June,0)),0) as June, " +
                                            " isnull(sum(isnull(July,0)),0) as July, isnull(sum(isnull(August,0)),0) as  August, isnull(sum(isnull(September,0)),0) as September, " +
                                            " isnull(sum(isnull(October,0)),0) as October, isnull(sum(isnull(November,0)),0) November, isnull(sum(isnull(December,0)),0) as December, " +
                                            " isnull(sum(isnull(January,0)),0) as January, isnull(sum(isnull(February,0)),0) as February, isnull(sum(isnull(March,0)),0) as March, Status " +
                                " into #Budget FROM #tempBudget " +
                                " group by Status, " + (DepartmentWise ? " isnull(Code,''), isnull(Name,'') " : " isnull(Department,'')") +

                                " drop table #tempTotalBudget drop table #tempYTDBudget drop table #tempYTDActuals drop table #tempBudget ";                               
                               
                query = query + " SELECT " + (DepartmentWise ? "Code, Name, " : " a.Department, ") + " cast(isnull(PYB,0) as decimal) as PYB, cast(isnull(YTDB,0) as decimal) as YTDB, cast(isnull(YTDA,0) as decimal) as YTDA, BudgetedAmount, " +
                                " April, May, June, " + (QuaterWise ? " (April+ May+ June) as '1stQuater'," : "") +
                                " July, August, September, " + (QuaterWise ? "(July+ August+ September) as '2ndQuater', " : "") +
                                " October,  November, December, " + (QuaterWise ? "(October+ November+ December) as '3rdQuater', " : "") +
                                " January, February, March, " + (QuaterWise ? "(January+ February+ March) as '4thQuater', " : "") +
                                " Status FROM  #Budget as a with(nolock) " +
                                " left join #TotalBudget as c on "+(DepartmentWise ? " a.[Code] = c.[G_L Account No_] " : " a.Department =  c.Department ") +
                                " left join #YTDBudget as d on "+(DepartmentWise ? " a.[Code] = d.[G_L Account No_] " : " a.Department =  d.Department ") +
                                " left join #YTDActuals as e on "+(DepartmentWise ? " a.[Code] = e.[G_L Account No_] " : " a.Department =  e.Department ") +
                                " order by "+ (DepartmentWise ? " isnull([Name],'')" : " isnull(a.Department,'') ") +
                                " drop table #TotalBudget  drop table #YTDActuals  drop table #YTDBudget drop table #Budget end";
                using (DataSet ds = DBM.GetDataSet(query))
                {
                    if (ds.Tables.Count > 0)
                    {
                        return ds.Tables[0];
                    }
                    else
                    {
                        DataTable dt = null;
                        return dt;
                    }
                }

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        #endregion
    }
}