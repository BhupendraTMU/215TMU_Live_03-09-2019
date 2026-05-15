using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for ForeCastDAO
/// </summary>
public class ForeCastDAO
{
    #region ForecastEntry
    public static DataSet GLAccountByBudgetName(string NavDbName, string Company, string DepartmentCode, string CampusCode, string BudgetId, string BudgetName, out string EmployeeId, out string ForecastStatus, out string Remark, out string ReviewedBy, out string ApprovedBy, string GLRange)
    {
        SqlCommand comd = null;
        EmployeeId = ForecastStatus = Remark = ReviewedBy = ApprovedBy = "";
        try
        {
            // CreateTable
            comd = DBM.GetCommandSP("CreateForeCastTable");
            comd.Parameters.AddWithValue("@Company", Company);
            DBM.WriteToDb(comd);

            string[] year = BudgetId.Split('-');
            string FebDays = Convert.ToInt32(year[1]) % 4 == 0 ? "29" : "28";
            
            // Insert GL In Table
            string query1 = "";
            string query =  " select No_ as [GL Code], Name as [GL Name]," +
                            " cast(0 as decimal) as Jan, cast(0 as decimal) as Feb, cast(0 as decimal) as March,cast(0 as decimal) as April," +
                            " cast(0 as decimal) as May, cast(0 as decimal) as June,cast(0 as decimal) as July, cast(0 as decimal) as August," +
                            " cast(0 as decimal) as Sept,cast(0 as decimal) as Oct, cast(0 as decimal) as Nov,  cast(0 as decimal) as December" +
                            " into #temp from " + NavDbName + "[" + Company + "$G_L Account] as a with(nolock) " +
                            " where a.No_ between " + GLRange + " and [Account Type] = 0 and a.No_ not in (select [GL Code] from [" + Company + "$Forecast] as b with(nolock) where b.Department = '" + DepartmentCode + "' and [Campus Code] = '" + CampusCode + "' and [Budget ID] = '" + BudgetId + "' and [Forecast Month] = " + DateTime.Now.Month.ToString() + " ) " +
                            
                            " update a set Jan = isnull(JanActual,0), Feb = isnull(FebActual,0), March = isnull(MarchActual,0), April = isnull(AprilActual,0), "+
                                        " May = isnull(MayActual,0), June = isnull(JuneActual,0), july = isnull(julyActual,0), August = isnull(AugustActual,0), "+
                                        " Sept = isnull(SeptActual,0), Oct = isnull(OctActual,0), Nov = isnull(NovActual,0), December=isnull(DecActual,0) " +
                            " from #temp as a " +
                            " inner join ("+
                            " select [G_L Account No_], "+
                                    " sum(case when [Posting Date] between '01/01/" + year[1].ToString() + "' and '01/31/" + year[1].ToString() + "' then isnull(cast(Amount as decimal),0) else 0 end) as JanActual, " +
                                    " sum(case when [Posting Date] between '02/01/" + year[1].ToString() + "' and '02/" + FebDays + "/" + year[1].ToString() + "' then isnull(cast(Amount as decimal),0) else 0 end) as FebActual, " +
                                    " sum(case when [Posting Date] between '03/01/" + year[1].ToString() + "' and '03/31/" + year[1].ToString() + "' then isnull(cast(Amount as decimal),0) else 0 end) as MarchActual, " +
                                    " sum(case when [Posting Date] between '04/01/" + year[0].ToString() + "' and '04/30/" + year[0].ToString() + "' then isnull(cast(Amount as decimal),0) else 0 end) as AprilActual, " +
                                    " sum(case when [Posting Date] between '05/01/" + year[0].ToString() + "' and '05/31/" + year[0].ToString() + "' then isnull(cast(Amount as decimal),0) else 0 end) as MayActual, " +
                                    " sum(case when [Posting Date] between '06/01/" + year[0].ToString() + "' and '06/30/" + year[0].ToString() + "' then isnull(cast(Amount as decimal),0) else 0 end) as JuneActual, " +
                                    " sum(case when [Posting Date] between '07/01/" + year[0].ToString() + "' and '07/31/" + year[0].ToString() + "' then isnull(cast(Amount as decimal),0) else 0 end) as JulyActual, " +
                                    " sum(case when [Posting Date] between '08/01/" + year[0].ToString() + "' and '08/31/" + year[0].ToString() + "' then isnull(cast(Amount as decimal),0) else 0 end) as AugustActual, " +
                                    " sum(case when [Posting Date] between '09/01/" + year[0].ToString() + "' and '09/30/" + year[0].ToString() + "' then isnull(cast(Amount as decimal),0) else 0 end) as SeptActual, " +
                                    " sum(case when [Posting Date] between '10/01/" + year[0].ToString() + "' and '10/31/" + year[0].ToString() + "' then isnull(cast(Amount as decimal),0) else 0 end) as OctActual, " +
                                    " sum(case when [Posting Date] between '11/01/" + year[0].ToString() + "' and '11/30/" + year[0].ToString() + "' then isnull(cast(Amount as decimal),0) else 0 end) as NovActual, " +
                                    " sum(case when [Posting Date] between '12/01/" + year[0].ToString() + "' and '12/31/" + year[0].ToString() + "' then isnull(cast(Amount as decimal),0) else 0 end) as DecActual " +
                            " from " + NavDbName + "[" + Company + "$G_L Entry] with(nolock) where [Global Dimension 1 Code] ='" + CampusCode + "' and [Global Dimension 2 Code] = '" + DepartmentCode + "'"+
                            " Group by [G_L Account No_]" +
                            " ) as b on a.[GL Code]=b.[G_L Account No_] " +

                            " Insert into  [" + Company + "$Forecast] " +
                            " ([Department], [Campus Code], [GL Code], [GL Name], [Budget Name], [Budget ID], [Forecast Month], [Forecast Amount],[Jan], [Feb], [March], [April], [May], [June], [July], [August], [Sept], [Oct], [Nov], [Dec],[Status Date]) " +
                            " select '" + DepartmentCode + "', '" + CampusCode + "', [GL Code], [GL Name],'" + BudgetName + "', '" + BudgetId + "', " + DateTime.Now.Month.ToString() + ","+
                            " (Jan + feb + March + April + May + June + july + August + Sept + Oct + Nov + December), Jan, feb, March, April, May, June, july, August, Sept, Oct, Nov, December, getdate() from #temp " +
                            " drop table #temp ";

            // Update Status Of GL Blocked or Not From Nav
            query = query + " update a set a.[Blocked] = b.[Blocked] " +
                            " from [" + Company + "$Forecast] as a inner join " + NavDbName + "[" + Company + "$G_L Account] as b with(nolock) on a.[GL Code] = b.No_ ";

            // Update Head Of Each GL
            query = query + " Select No_ as HeadCode,REPLACE(Totaling,'..',' and ') As Range from " + NavDbName + "[" + Company + "$G_L Account] where No_ between " + GLRange + " and [Account Type] = 4";
            
            foreach (DataRow dr in DBM.GetDataSet(query).Tables[0].Rows)
            {
                query1 = query1 + " update [" + Company + "$Forecast] set [Head Code] = '" + dr["HeadCode"] + "' where [GL Code] between " + dr["Range"];
            }

            DBM.WriteToDb(query1);

            // Get Data Head Wise
            using (DataSet ds = GLHead(NavDbName, Company, DepartmentCode, CampusCode, BudgetId))
            {
                EmployeeId = ds.Tables[1].Rows[0]["EmployeeId"].ToString();
                ForecastStatus = ds.Tables[1].Rows[0]["Status"].ToString();
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

            String CurrentMonth = DateTime.Now.Month.ToString();
            string query = "Declare @PreviousBudget varchar(20) select @PreviousBudget = max(Name) from " + NavDbName + "[" + Company + "$G_L Budget Name] with(nolock) where Name < '" + BudgetId + "' " + 
                        "select [GL Code] as GLCode, "+
                                    " sum(case([Forecast Month]) when 4 then April when 5 then May when 6 then june when 7 then July " +
                                                              " when 8 then August when 9 then Sept when 10 then Oct when 11 then Nov when 12 then Dec " +
                                                              " when 1 then Jan when 2 then Feb when 3 then March else 0 end) as PYF, " +
                                    " sum(case when ([Forecast Month] = 4 and "+  CurrentMonth  +" != 4) then April when ([Forecast Month] = 5 and ("+ CurrentMonth +" > 5 or "+ CurrentMonth +" < 4)) then May " +
                                             " when ([Forecast Month] = 6 and ("+ CurrentMonth +" > 6 or "+ CurrentMonth +" < 4)) then june when ([Forecast Month] = 7 and ("+ CurrentMonth +" > 7 or "+ CurrentMonth +" < 4)) then July " +
                                             " when ([Forecast Month] = 8 and ("+ CurrentMonth +" > 8 or "+ CurrentMonth +" < 4)) then August when ([Forecast Month] = 9 and ("+ CurrentMonth +" > 9 or "+ CurrentMonth +" < 4)) then Sept " +
                                             " when ([Forecast Month] = 10 and ("+ CurrentMonth +" > 10 or "+ CurrentMonth +" < 4)) then Oct when ([Forecast Month] = 11 and ("+ CurrentMonth +" > 11 or "+ CurrentMonth +" < 4)) then Nov " +
                                             " when ([Forecast Month] = 12 and "+ CurrentMonth +" < 4) then Dec when ([Forecast Month] = 1 and "+ CurrentMonth +" < 4 and "+ CurrentMonth +" > 1) then Jan " +
                                             " when ([Forecast Month] = 2 and "+ CurrentMonth +" < 4 and "+ CurrentMonth +" >2) then Feb  else 0 end) as YTDF "+ //-- when ([Forecast Month] = 3 and "+ CurrentMonth +" = 3) then March //NotRequired Because When Current month Will be April Then There Will Be No Month of Previous Year Coz Financial Year Start From April
                          " into #PreviousYearDetails"+
                          " from [" + Company + "$Forecast] as a with(nolock) " +
                          " group by [GL Code],[Budget ID],Department,[Campus Code] " +
                          " having [Budget ID] = @PreviousBudget and Department = '" + DepartmentCode + "' and [Campus Code] = '" + CampusCode + "'";

            query = query + " select isnull(sum(isnull(a.Amount,0)),0) as YTDA,a.[G_L Account No_] " +
                               " into #tempYTDActuals " +
                               " from " + NavDbName + "[" + Company + "$G_L Entry] as a with(nolock) " +
                               " group by a.[G_L Account No_] ,a.[Global Dimension 1 Code], a.[Global Dimension 2 Code],a.[Posting Date] " +
                               " having a.[Global Dimension 1 Code] = '" + CampusCode + "' and a.[Global Dimension 2 Code] = '" + DepartmentCode + "' " +
                               " and cast(Convert(varchar(10),a.[Posting Date],101) as date) between " +
                                                       " cast('" + PreviousFinancialStartDate + "' as date) and cast(CONVERT(varchar(10),DATEADD(s,-1,DATEADD(mm, DATEDIFF(m,0,cast(cast(Datepart(m,getdate()) as varchar)+'/01/'+ case when (Datepart(m,getdate()) > 3) then '" + (Convert.ToInt32(year[0]) - 1).ToString() + "' else '" + year[0] + "' end as date)),0)),101) as date) " +
                                " select isnull(sum(isnull(YTDA,0)),0) as YTDA,[G_L Account No_] into #YTDActuals from #tempYTDActuals group by [G_L Account No_]" +
                                " drop table #tempYTDActuals";

            query = query + " select Replace(Totaling,'..',' and ') as GLRange,a.[Head Code] as Code,b.Name, isnull(sum(isnull(c.PYF,0)),0) as PYF, isnull(sum(isnull(c.YTDF,0)),0) as YTDF, cast(isnull(sum(isnull(d.YTDA,0)),0) as decimal) as YTDA, " +
                            " isnull(sum(isnull(a.[Forecast Amount],0)),0) as ForecastedAmount,isnull(sum(isnull(a.April,0)),0) as April,isnull(sum(isnull(a.May,0)),0) as May,isnull(sum(isnull(a.June,0)),0) as June, " +
                            " isnull(sum(isnull(a.July,0)),0) as July,isnull(sum(isnull(a.August,0)),0) as August,isnull(sum(isnull(a.Sept,0)),0) as Sept,isnull(sum(isnull(a.Oct,0)),0) as Oct, " +
                            " isnull(sum(isnull(a.Nov,0)),0) as Nov,isnull(sum(isnull(a.Dec,0)),0) as Dec,isnull(sum(isnull(a.Jan,0)),0) as Jan,isnull(sum(isnull(a.Feb,0)),0) as Feb, " +
                            " isnull(sum(isnull(a.March,0)),0) as March " +
                            " from [" + Company + "$Forecast] as a with(nolock) " +
                            " left join #PreviousYearDetails as c with(nolock) on a.[GL Code] = c.GLCode " +
                            " left join #YTDActuals as d with(nolock) on a.[GL Code] =  d.[G_L Account No_] " +
                            " inner join " + NavDbName + "[" + Company + "$G_L Account] as b with(nolock) on a.[Head Code] = b.No_ " +
                            " group by a.[head code],b.Name,[Budget ID],Department,[Campus Code],Totaling,[Forecast Month],a.Blocked " +
                            " having [Budget ID] = '" + BudgetId + "' and Department = '" + DepartmentCode + "' and [Campus Code] = '" + CampusCode + "' and [Forecast Month] = " + CurrentMonth + " and a.[Head Code] is not null and a.Blocked = 0 " +
                            " Drop table #PreviousYearDetails drop table #YTDActuals " +

                            " Select top 1 isnull([Employee ID],'') as EmployeeId, isnull([Reviewed By],'') as ReviewedBy, isnull([Approved By],'') as ApprovedBy, isnull([Status],'') as Status, isnull(Remarks,'') as Remark FROM  [dbo].[" + @Company + "$Forecast] with(nolock) " +
                            " where [Budget ID] = '" + BudgetId + "' and Department = '" + DepartmentCode + "' and [Campus Code] = '" + CampusCode + "' and [Forecast Month] = " + CurrentMonth + "";
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

            String CurrentMonth = DateTime.Now.Month.ToString();
            string query = "Declare @PreviousBudget varchar(20) select @PreviousBudget = max(Name) from " + NavDbName + "[" + Company + "$G_L Budget Name] with(nolock) where Name < '" + BudgetId + "' " +
                        "select [GL Code] as GLCode, " +
                                    " sum(case([Forecast Month]) when 4 then April when 5 then May when 6 then june when 7 then July " +
                                                              " when 8 then August when 9 then Sept when 10 then Oct when 11 then Nov when 12 then Dec " +
                                                              " when 1 then Jan when 2 then Feb when 3 then March else 0 end) as PYF, " +
                                    " sum(case when ([Forecast Month] = 4 and " + CurrentMonth + " != 4) then April when ([Forecast Month] = 5 and (" + CurrentMonth + " > 5 or " + CurrentMonth + " < 4)) then May " +
                                             " when ([Forecast Month] = 6 and (" + CurrentMonth + " > 6 or " + CurrentMonth + " < 4)) then june when ([Forecast Month] = 7 and (" + CurrentMonth + " > 7 or " + CurrentMonth + " < 4)) then July " +
                                             " when ([Forecast Month] = 8 and (" + CurrentMonth + " > 8 or " + CurrentMonth + " < 4)) then August when ([Forecast Month] = 9 and (" + CurrentMonth + " > 9 or " + CurrentMonth + " < 4)) then Sept " +
                                             " when ([Forecast Month] = 10 and (" + CurrentMonth + " > 10 or " + CurrentMonth + " < 4)) then Oct when ([Forecast Month] = 11 and (" + CurrentMonth + " > 11 or " + CurrentMonth + " < 4)) then Nov " +
                                             " when ([Forecast Month] = 12 and " + CurrentMonth + " < 4) then Dec when ([Forecast Month] = 1 and " + CurrentMonth + " < 4 and " + CurrentMonth + " > 1) then Jan " +
                                             " when ([Forecast Month] = 2 and " + CurrentMonth + " < 4 and " + CurrentMonth + " >2) then Feb  else 0 end) as YTDF " + //-- when ([Forecast Month] = 3 and "+ CurrentMonth +" = 3) then March //NotRequired Because When Current month Will be April Then There Will Be No Month of Previous Year Coz Financial Year Start From April
                          " into #PreviousYearDetails" +
                          " from [" + Company + "$Forecast] as a with(nolock) " +
                          " group by [GL Code],[Budget ID],Department,[Campus Code] " +
                          " having [Budget ID] = @PreviousBudget and Department = '" + DepartmentCode + "' and [Campus Code] = '" + CampusCode + "'";

            query = query + " select isnull(sum(isnull(a.Amount,0)),0) as YTDA,a.[G_L Account No_] " +
                               " into #tempYTDActuals " +
                               " from " + NavDbName + "[" + Company + "$G_L Entry] as a with(nolock) " +
                               " group by a.[G_L Account No_] ,a.[Global Dimension 1 Code], a.[Global Dimension 2 Code],a.[Posting Date] " +
                               " having a.[Global Dimension 1 Code] = '" + CampusCode + "' and a.[Global Dimension 2 Code] = '" + DepartmentCode + "' " +
                               " and cast(Convert(varchar(10),a.[Posting Date],101) as date) between " +
                                                       " cast('" + PreviousFinancialStartDate + "' as date) and cast(CONVERT(varchar(10),DATEADD(s,-1,DATEADD(mm, DATEDIFF(m,0,cast(cast(Datepart(m,getdate()) as varchar)+'/01/'+ case when (Datepart(m,getdate()) > 3) then '" + (Convert.ToInt32(year[0]) - 1).ToString() + "' else '" + year[0] + "' end as date)),0)),101) as date) " +
                                " select isnull(sum(isnull(YTDA,0)),0) as YTDA,[G_L Account No_] into #YTDActuals from #tempYTDActuals group by [G_L Account No_]" +
                                " drop table #tempYTDActuals";


            query = query + " SELECT [id] as ID, isnull([GL Code],'') as Code, isnull([GL Name],'') as Name, cast(isnull(PYF,0) as decimal) as PYF, cast(isnull(YTDF,0) as decimal) as YTDF, cast(isnull(YTDA,0) as decimal) as YTDA, isnull([Forecast Amount],0) as ForecastedAmount," +
                                " isnull([Jan],0) as Jan, isnull([Feb],0) as Feb, isnull([March],0) as March, isnull([April],0) as April, isnull([May],0) as May, isnull([June],0) as June," +
                                " isnull([July],0) as July, isnull([August],0) as  Aug,isnull([Sept],0) as Sept, isnull([Oct],0) as Oct, isnull([Nov],0) Nov, isnull([Dec],0) as Dec " +
                                " FROM  [dbo].[" + @Company + "$Forecast] as a  with(nolock) " +
                                " left join #PreviousYearDetails as b on a.[GL Code] = b.GLCode " +
                                " left join #YTDActuals as c on a.[GL Code] = c.[G_L Account No_] " +
                                " where [Budget ID] = '" + BudgetId + "' and Department = '" + DepartmentCode + "' and [Campus Code] = '" + CampusCode + "' and [Forecast Month] = " + DateTime.Now.Month.ToString() + " and [GL Code] between " + GLRange + " and Blocked = 0" +
                                " order by isnull([GL Name],'') " +
                                " drop table #PreviousYearDetails drop table #YTDActuals";
            return DBM.GetDataSet(query);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    public static void UpdateForecastDetails(ForeCastDTO objDTO, string EmployeeId)
        {
            try
            {
                string query = "update ["+objDTO.CompanyName+ "$Forecast]"+
				                        "set Jan = '" +objDTO.Jan.ToString()+ "', Feb = '" +objDTO.Feb.ToString()+ "', March = '" +objDTO.March.ToString()+ "',"+
                                        "April = '" +objDTO.April.ToString()+ "', May = '" +objDTO.May.ToString()+ "', June = '" +objDTO.June.ToString()+ "',"+
                                        "July = '" +objDTO.July.ToString()+ "', August = '" +objDTO.Aug.ToString()+ "', Sept = '" +objDTO.Sept.ToString()+ "',"+
                                        "Oct = '" +objDTO.Oct.ToString()+ "', Nov = '" +objDTO.Nov.ToString()+ "', Dec = '" +objDTO.Dec.ToString()+ "',"+ 
                                        "[Forecast Amount] = '" +objDTO.ForeCastedAmount.ToString()+ "', [Edit Employee ID] = '" + EmployeeId +"'"+
		                            "where id = '" +objDTO.Id+ "'";
                DBM.WriteToDb(query);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    public static void ForecastSendForReview(string CompanyName, string BudgetId, string DepartmentCode, string CampusCode, string EmployeeId)
    {
        try
        {
            string query = "update [" + CompanyName + "$Forecast] set [Employee ID] = '" + EmployeeId + "', [Created Date] =  GETDATE(), [Status] = 'PROCESSING', [Status Date] = getdate()" +
                                "where [Budget ID] = '" + BudgetId + "' and Department = '" + DepartmentCode + "' and [Campus Code] = '" + CampusCode + "' and [Forecast Month] = " + DateTime.Now.Month.ToString() + "  and Blocked = 0 ";
            DBM.WriteToDb(query);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }
    public static void ForecastSendForApproval(string CompanyName, string BudgetId, string DepartmentCode, string CampusCode, string Remark, string EmployeeId)
    {
        try
        {
            string query = "update [" + CompanyName + "$Forecast] set Remarks = '" + Remark + "', [Status Date] = GETDATE(), " +
                                            " [Reviewed By] = '" + EmployeeId + "', [Reviewed Date] =  GETDATE(), [Status] = 'REVIEWED' " +
                            " where [Budget ID] = '" + BudgetId + "' and Department = '" + DepartmentCode + "' and [Campus Code] = '" + CampusCode + "' and [Forecast Month] = " + DateTime.Now.Month.ToString() + "  and Blocked = 0 ";
            DBM.WriteToDb(query);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }
    public static void ForecastApprove(string CompanyName, string BudgetId, string DepartmentCode, string CampusCode, string Remark, string EmployeeId)
    {
        try
        {
            string Query = " update [" + CompanyName + "$Forecast] set Remarks = '" + Remark + "', [Status Date] =  GETDATE(), [Status] = 'APPROVED', [Approved By] = '" + EmployeeId + "',  [Approved Date] = getdate()" +
                           " where [Budget ID] = '" + BudgetId + "' and Department = '" + DepartmentCode + "' and [Campus Code] = '" + CampusCode + "' and [Forecast Month] = " + DateTime.Now.Month.ToString() + "  and Blocked = 0 ";
            DBM.WriteToDbWithTransaction(Query);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }
    public static void ForecastReject(string CompanyName, string BudgetId, string DepartmentCode, string CampusCode, string Remark, string EmployeeId)
    {
        try
        {
            string Query = " update [" + CompanyName + "$Forecast] set Remarks = '" + Remark + "', [Status Date] = GETDATE(), " +
                                    " [Reviewed By] = case(upper([Status])) when 'PROCESSING' then '" + EmployeeId + "' else [Reviewed By] end, " +
                                    " [Reviewed Date] = case(upper([Status])) when 'PROCESSING' then getdate() else [Reviewed Date] end, " +
                                    " [Approved By] = case(upper([Status])) when 'REVIEWED' then '" + EmployeeId + "' else NULL end, " +
                                    " [Approved Date] = case(upper([Status])) when 'REVIEWED' then getdate() else NULL end, " +
                                    " [Status] = 'SENTBACK'" +
                               " where [Budget ID] = '" + BudgetId + "' and Department = '" + DepartmentCode + "' and [Campus Code] = '" + CampusCode + "' and [Forecast Month] = " + DateTime.Now.Month.ToString() + "  and Blocked = 0 ";

            DBM.WriteToDbWithTransaction(Query);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }
    public static string GetToMailIds(string Prefix, string Company, string BudgetId, string Department, string CampusCode, string ForecastStatus, string ForecastScreen)
    {
        try
        {
            string Query = "";
            //if (ForecastStatus == "PROCESSING" || ForecastStatus == "REVIEWED")
            //{
            //    Query = " select distinct [Company E-Mail] as Id from " + Prefix + "$Employee] as a with(nolock) " +
            //            " where (a.No_ in (select b.No_ from " + Prefix + "$Employee] as b with(nolock) where b.[Department Code] = '" + Department + "') or a.No_ in (select b.HOD from " + Prefix + "$Employee] as b with(nolock) where b.[Department Code] = '" + Department + "')) " +
            //            " and " + (ForecastStatus == "PROCESSING" ? "[Budget Review] = 1" : " [Budget Approve] = 1") + "";
            //}
            if (ForecastStatus == "PROCESSING" || ForecastStatus == "REVIEWED")
            {
                Query = " select distinct [Company E-Mail] as Id from " + Prefix + "$Employee] as a with(nolock) " +
                           " where (a.No_ in (select [HOD-1] from " + Prefix + "$Department Master] as b with(nolock) where b.[Department Code] = '" + Department + "') or a.No_ in (select [HOD-2] from " + Prefix + "$Department Master] as b with(nolock) where b.[Department Code] = '" + Department + "')) " +
                           " and " + (ForecastStatus == "PROCESSING" ? "[Budget Review] = 1" : " [Budget Approve] = 1") + "";
            }
            else if (ForecastStatus == "APPROVED")
            {
                Query = " Declare @EmployeeId varchar(50), @ReviewedId varchar(50)   select @EmployeeId = [Employee ID],@ReviewedId = [Reviewed By]  from [" + Company + "$Forecast] with(nolock) where [Budget ID] = '" + BudgetId + "' and [Campus Code] = '" + CampusCode + "' and Department = '" + Department + "' and [Forecast Month] = " + DateTime.Now.Month.ToString() +
                        " select [Company E-Mail] as Id from " + Prefix + "$Employee] with(nolock) where No_ in (@EmployeeId,@ReviewedId)";
            }
            else if (ForecastStatus == "SENTBACK")
            {
                Query = "select [Company E-Mail] as Id from " + Prefix + "$Employee] with(nolock) where No_ in (select " + (ForecastScreen == "REVIEW" ? " [Employee ID]" : "[Reviewed By]") + " from [" + Company + "$Forecast] with(nolock) where [Budget ID] = '" + BudgetId + "' and [Campus Code] = '" + CampusCode + "' and Department = '" + Department + "' and [Forecast Month] = " + DateTime.Now.Month.ToString() + ")";
            }
            string ToId = "";
            using (SqlDataReader rdr = DBM.getReader(Query))
            {
                if (rdr.HasRows)
                {
                    while (rdr.Read())
                    {
                        ToId += rdr["Id"].ToString() + ",";
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

    #region ForeCastReport  
    public static DataTable rptForecastByDepartment(string NavDbName, string Company, string BudgetId, string DepartmentCode, string CampusCode, Int32 ForecastMonth, bool DepartmentWise, bool QuaterWise, string EmployeeDepartments)
    {
        try
        {
            string[] year = BudgetId.Split('-');
            string PreviousFinancialStartDate = "04/01/" + (Convert.ToInt32(year[0].Trim()) - 1).ToString();
            // Previous Year details
            string query = "IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].["+Company+"$Forecast]') AND type in (N'U')) begin" +
                            " Declare @PreviousBudget varchar(20) select @PreviousBudget = max(Name) from " + NavDbName + "[" + Company + "$G_L Budget Name] with(nolock) where Name < '" + BudgetId + "' " +
                            " select [GL Code] as GLCode,Department, " +
                                    " case([Forecast Month]) when 4 then April when 5 then May when 6 then june when 7 then July " +
                                                              " when 8 then August when 9 then Sept when 10 then Oct when 11 then Nov when 12 then Dec " +
                                                              " when 1 then Jan when 2 then Feb when 3 then March else 0 end as PYF, " +
                                    " case when ([Forecast Month] = 4 and " + ForecastMonth + " != 4) then April when ([Forecast Month] = 5 and (" + ForecastMonth + " > 5 or " + ForecastMonth + " < 4)) then May " +
                                             " when ([Forecast Month] = 6 and (" + ForecastMonth + " > 6 or " + ForecastMonth + " < 4)) then june when ([Forecast Month] = 7 and (" + ForecastMonth + " > 7 or " + ForecastMonth + " < 4)) then July " +
                                             " when ([Forecast Month] = 8 and (" + ForecastMonth + " > 8 or " + ForecastMonth + " < 4)) then August when ([Forecast Month] = 9 and (" + ForecastMonth + " > 9 or " + ForecastMonth + " < 4)) then Sept " +
                                             " when ([Forecast Month] = 10 and (" + ForecastMonth + " > 10 or " + ForecastMonth + " < 4)) then Oct when ([Forecast Month] = 11 and (" + ForecastMonth + " > 11 or " + ForecastMonth + " < 4)) then Nov " +
                                             " when ([Forecast Month] = 12 and " + ForecastMonth + " < 4) then Dec when ([Forecast Month] = 1 and " + ForecastMonth + " < 4 and " + ForecastMonth + " > 1) then Jan " +
                                             " when ([Forecast Month] = 2 and " + ForecastMonth + " < 4 and " + ForecastMonth + " >2) then Feb  else 0 end as YTDF " + //-- when ([Forecast Month] = 3 and "+ CurrentMonth +" = 3) then March //NotRequired Because When Current month Will be April Then There Will Be No Month of Previous Year Coz Financial Year Start From April
                          " into #tempPreviousYearDetails" +
                          " from [" + Company + "$Forecast] as a with(nolock) " +
                          " where [Budget ID] = @PreviousBudget and " + (DepartmentWise ? " Department " : " [GL Code] ") + " in (select * from dbo.split('" + DepartmentCode + "')) " + (CampusCode == "0" ? "" : " and [Campus Code] = '" + CampusCode + "'");

            query = query + " select [G_L Account No_],[Global Dimension 2 Code],isnull(Amount,0) as YTDA " +
                            " into #tempYTDActuals " +
                            " from " + NavDbName + "[" + Company + "$G_L Entry] with(nolock) " +
                            " where " + (DepartmentWise ? " [Global Dimension 2 Code] " : " [G_L Account No_] ") + " in (select * from dbo.split('" + DepartmentCode + "')) " + (CampusCode == "0" ? "" : " and [Global Dimension 1 Code] = '" + CampusCode + "'") +
                            " and cast(Convert(varchar(10),[Posting Date],101) as date) between " +
                                                    " cast('" + PreviousFinancialStartDate + "' as date) and cast(CONVERT(varchar(10),DATEADD(s,-1,DATEADD(mm, DATEDIFF(m,0,cast('" + ForecastMonth + "'+'/01/'+ case when (" + ForecastMonth + " > 3) then '" + (Convert.ToInt32(year[0]) - 1).ToString() + "' else '" + year[0] + "' end as date)),0)),101) as date) ";
            // Current Year Details
            query = query + " SELECT isnull([GL Code],'') as GLCode, isnull([GL Name],'') as Name, isnull(Department,'') as Department, " +
                                        " isnull([Forecast Amount],0) as ForecastedAmount, isnull([April],0) as April, isnull([May],0) as May, isnull([June],0) as June, " +
                                        " isnull([July],0) as July, isnull([August],0) as  August, isnull([Sept],0) as September, " +
                                        " isnull([Oct],0) as October, isnull([Nov],0) November, isnull([Dec],0) as December, " +
                                        " isnull([Jan],0) as January, isnull([Feb],0) as February, isnull([March],0) as March, Status " +
                                " into #tempForecast" +
                                " FROM  [" + Company + "$Forecast] with(nolock) " +
                                " where [Budget ID] = '" + @BudgetId + "'" + (CampusCode == "0" ? "" : " and [Campus Code] = '" + CampusCode + "'") + " and " + (DepartmentWise ? " Department " : " [GL Code] ") + " in (select * from dbo.split('" + DepartmentCode + "')) " + (DepartmentWise ? "" : " and Department in (select * from dbo.Split('" + EmployeeDepartments + "')) ") + " and [Forecast Month] = " + ForecastMonth.ToString() + " and Blocked = 0 ";
          
            // Grouping By either [G_L Account No_] or Department
            query = query + " select isnull(sum(isnull(PYF,0)),0) as PYF,isnull(sum(isnull(YTDF,0)),0) as YTDF," + (DepartmentWise ? " GLCode " : " Department ") + " into #PreviousYearDetails from #tempPreviousYearDetails group by " + (DepartmentWise ? " GLCode " : " Department ") +
                            " select isnull(sum(isnull(YTDA,0)),0) as YTDA," + (DepartmentWise ? " [G_L Account No_] " : " [Global Dimension 2 Code] as Department ") + " into #YTDActuals from #tempYTDActuals group by " + (DepartmentWise ? " [G_L Account No_] " : " [Global Dimension 2 Code] ") +

                            " SELECT " + (DepartmentWise ? " isnull(GLCode,'') as GLCode, isnull(Name,'') as Name, " : " isnull(Department,'') as Department, ") + " isnull(sum(isnull(ForecastedAmount,0)),0) as ForecastedAmount, " +
                                        " isnull(sum(isnull(April,0)),0) as April, isnull(sum(isnull(May,0)),0) as May, isnull(sum(isnull(June,0)),0) as June, " +
                                        " isnull(sum(isnull(July,0)),0) as July, isnull(sum(isnull(August,0)),0) as  August, isnull(sum(isnull(September,0)),0) as September, " +
                                        " isnull(sum(isnull(October,0)),0) as October, isnull(sum(isnull(November,0)),0) November, isnull(sum(isnull(December,0)),0) as December, " +
                                        " isnull(sum(isnull(January,0)),0) as January, isnull(sum(isnull(February,0)),0) as February, isnull(sum(isnull(March,0)),0) as March, Status " +
                            " into #Forecast FROM #tempForecast " +
                            " group by Status, " + (DepartmentWise ? " isnull(GLCode,''), isnull(Name,'') " : " isnull(Department,'')") +

                            " drop table #tempPreviousYearDetails drop table #tempYTDActuals drop table #tempForecast ";

            query = query + " SELECT " + (DepartmentWise ? "a.GLCode as Code, Name, " : " a.Department, ") + " cast(isnull(PYF,0) as decimal) as PYF, cast(isnull(YTDF,0) as decimal) as YTDF, cast(isnull(YTDA,0) as decimal) as YTDA, ForecastedAmount, " +
                            " April, May, June, " + (QuaterWise ? " (April+ May+ June) as '1stQuater'," : "") +
                            " July, August, September, " + (QuaterWise ? "(July+ August+ September) as '2ndQuater', " : "") +
                            " October,  November, December, " + (QuaterWise ? "(October+ November+ December) as '3rdQuater', " : "") +
                            " January, February, March, " + (QuaterWise ? "(January+ February+ March) as '4thQuater', " : "") +
                            " Status FROM  #Forecast as a with(nolock) " +
                            " left join #PreviousYearDetails as c on " + (DepartmentWise ? " a.[GLCode] = c.GLCode " : " a.Department =  c.Department ") +
                            " left join #YTDActuals as e on " + (DepartmentWise ? " a.[GLCode] = e.[G_L Account No_] " : " a.Department =  e.Department ") +
                            " order by " + (DepartmentWise ? " isnull([Name],'')" : " isnull(a.Department,'') ") +
                            " drop table #YTDActuals  drop table #PreviousYearDetails drop table #Forecast end";
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