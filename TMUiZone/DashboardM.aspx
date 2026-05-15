<%@ Page Title="" Language="C#" MasterPageFile="~/MasterContent.master" AutoEventWireup="true" CodeFile="DashboardM.aspx.cs" Inherits="DashboardM" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <LINK rel="STYLESHEET" type="text/css" href="dhtmlwindow.css">
	<LINK rel="STYLESHEET" type="text/css" href="modal.css">
	<script type="text/javascript" src="dhtmlwindow.js"></script>
	<script type="text/javascript" src="modal.js"></script>
	
    <script language="JavaScript" src="popup.js">
        //Popup box II 
	</script>
	<style type="text/css">
		.Shadow
		{
		border: solid 1px #336699;
		border-collapse: collapse;
		background-color: White;
		margin-bottom: 2px; 
		filter: progid:DXImageTransform.Microsoft.Shadow(color="#141414",Direction=135, Strength=8);
		}
		.Gradient
		{
		border: solid 0px #336699;
		font: normal 11px/1.5em Verdana;
		border-collapse: collapse;	
		margin-bottom: 10px; 
		filter:progid:DXImageTransform.Microsoft.gradient(startColorstr=#FFCC00, endColorstr=white);
		}
	</style>
	
	<script language="javascript" type="text/javascript">
	    function openWindow() {
	        win = dhtmlmodal.open('winbox', 'iFrame', 'Sangathan/msgBoard.aspx', 'Sangathan Message Board', 'width=420px,height=300px,resize=1,scrolling=0,center=1');
	    }

	    function openWindowCat() {
	        win = dhtmlmodal.open('winbox', 'iFrame', 'AICERegistrations.aspx', 'CAT 2012', 'width=420px,height=280px,resize=1,scrolling=0,center=1');
	    }

	</script>	 
	  



</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

      <div style="width:830px">
        



    <table width="100%" border="0" cellpadding="0" cellspacing="0">
        <tr>
            <td width="72%" valign="top">
                <table width="100%" border="0">
                    <tr>
                        <td height="280" align="center">
                            <b class="b1"></b><b class="b2"></b><b class="b3"></b><b class="b4"></b>
                            <div class="contentb">
                                <div>
                                    <table width="100%" height="290" border="0" cellpadding="4" cellspacing="0">
                                        <tr>
                                            <td valign="top">
                                                <table width="100%" border="0" align="center" cellpadding="4" cellspacing="0">
                                                   <tr> <td>
                                                       <img src="images/3.jpg" /> </td></tr>
                                                                                                        
                                                </table>					
												
                                            </td>
                                        </tr>
                                    </table>
										<div align="right">
											
											<!-- <a id="ctl00_ContentPlaceHolder1_HyperLink1" style="font-size:Large;">[HyperLink1]</a> -->
										</div>
                                </div>
                            </div>
                            <b class="b4"></b><b class="b3"></b><b class="b2"></b><b class="b1"></b>
                        </td>
                    </tr>
                    <tr>
                        <td>
                           
                        </td>
                    </tr>
					<tr>
					<td>
						<table width="100%" border="0" cellpadding="0" cellspacing="1">
						<tr>
							<td width="33%" valign="top" class="headerColor">
								<b class="b1h"></b><b class="b2h"></b><b class="b3h"></b><b class="b4h"></b>
								<div class="headh" style="background-color:Orange">
									<strong>
										<center>
											Today`s Thought / <font color="#660000">Quote</font></center>
									</strong>
								</div>
								<div class="contenth">
									<div>
										<table width="95%" border="0" align="center" cellpadding="4" cellspacing="0" height="175px">
											<tr>
												<td valign="top">
													<strong>Thought :</strong> <span id="ctl00_ContentPlaceHolder1_lblThought">When the problems mount so hig that you cannot see anything else, it pays to step back from your work so you can see the big picture.</span>
													<br />
													<br />
													<script src="https://www.brainyquote.com/link/quotebr.js" type="text/javascript"></script>
												</td>
											</tr>
										</table>
										  
									</div>
								</div>
								<b class="b4bh"></b><b class="b3bh"></b><b class="b2bh"></b><b class="b1h"></b>
							</td>
							<td width="33%" valign="top" class="headerColor">
								<b class="b1hGreen"></b><b class="b2hGreen"></b><b class="b3hGreen"></b><b class="b4hGreen">
								</b>
								<div class="headhGreen" style="background-color:Lime">
									<strong>
										<center>
											Digital Library</center>
									</strong>
								</div>
								<div class="contenthGreen">
									<div>
										<table width="95%" border="0" align="center" cellpadding="4" cellspacing="0" height="175px">
											<tr>
												<td width="10" valign="top">
													<img src="images/right39.gif" width="9" height="9" />
												</td>
												<td align="left">
													<a class="Home" href="">EBSCO Online : Business Journal </a>
													<br>
													2,300 journals, more than 1,100 peer-reviewed titles, LISTA - 500 core journals..</a>
												</td>
											</tr>
											<tr>
												<td width="10">
													<img src="images/right39.gif" width="9" height="9" />
												</td>
												<td align="left">
													<a class="Home" href="" >Cambridge University Press (223)</a>
												</td>
											</tr>
											<tr>
												<td width="10">
													<img src="images/right39.gif" width="9" height="9" />
												</td>
												<td align="left">
													<a class="Home" href="">Taylor &amp; Francis : (1076) </a>
												</td>
											</tr>
											<tr>
												<td width="10">
													<img src="images/right39.gif" width="9" height="9" />
												</td>
												<td align="left">
													<a class="Home" href="">OxFord University Press : (198) </a>
												</td>
											</tr>
											<tr>
												<td>
													&nbsp;
												</td>
												<td align="right">
													<a class="Home" href="">
														<img src="images/more_icons.gif" width="15" height="15" border="0" />more..</a>
												</td>
											</tr>
										</table>
									</div>
								</div>
								<b class="b4bhGreen"></b><b class="b3bhGreen"></b><b class="b2bhGreen"></b><b class="b1hGreen">
								</b>
							</td>							
						</tr>
							
					</table>
					</td>
					</tr>

                </table>
            </td>
            <td valign="top" align="top">
                <table border="0" cellpadding="4" cellspacing="2">
                    <tr>
                        <td valign=top>
                           
                           <div id="Div1" style="vertical-align:top">                            
                               <div id="ctl00_ContentPlaceHolder1_panelring">
                               
                                   <br />
                               
                                   <img src="images/4.jpg" width="145px"/><br /> <br />
                                   <img src="images/5.png" width="145px" />
                                <%--   <a href="AUring/Default.aspx" target="_blank">
                                   &nbsp;</a><br /><br />
                               --%>
</div>
                            </div>
                            
                            
                          <%--  <div id="shadetabsRB">
                                <ul id="countrytabsRB" class="shadetabs">
                                    <li><a href="" rel="#iframe" class="selected"><span>Notices</span></a></li>
                                            
                                    <li><a href="" rel="#iframe"><span>President Msg.</span></a></li>
                                            
                                </ul>
                                <b class="b1AKCDS1"></b><b class="b2AKCDS1"></b><b class="b3AKCDS1"></b><b class="b4AKCDS1">
                                </b>
                                <div class="contentbAKCDS1">
                                    <div id="countrydivcontainerRB" style="height:270px;border: 0px solid gray; width: 200px; background-color: white;
                                        margin-bottom: 0px; padding: 0px">
                                    </div>
                                </div>
                                <b class="b4AKCDS1"></b><b class="b3AKCDS1"></b><b class="b2AKCDS1"></b><b class="b1AKCDS1">
                                </b>

                                <script type="text/javascript">

                                    var countries = new ddajaxtabs("countrytabsRB", "countrydivcontainerRB")
                                    countries.setpersist(true)
                                    countries.setselectedClassTarget("link") //"link" or "linkparent"
                                    countries.init()
                                </script>

                            </div>--%>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
		<!-- Sangathan starts here -->
       
		<!-- Sangathan Ends here -->

        <tr>
            <td colspan="2">&nbsp;
                &nbsp;
            </td>
        </tr>
        <tr id="ctl00_ContentPlaceHolder1_trcat">
	<td colspan="3">
		   <%-- <a href="ja vascript:void(0);" onclick="openWindowCat();" >
		    <img src="images/newOne.png" border="0" /></a>--%>
		    </td>
</tr>

    </table>

    </div>
</asp:Content>

