using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OracleClient;
using System.Linq;
using System.Web;

namespace DDWebAPI.Models
{
    public class BillModel
    {
        public string nodeKeyProperty = "id";
        public nodeData[] nodeDataArray;
        public linkData[] linkDataArray;

        public BillModel getData(string ClaimNumber)
        {
            BillModel BMData = new BillModel();
            int id=0;
            string transaction;
            string node;
            int left = 50, top = 50;
            string Sql = "select * from (select a.*, rownum rnum from ((select e.Transaction_NAME, e.TRANSACTION_TYPE, c.work_item_id, MITCHELL_COMPANY_CODE, MITCHELL_USER_ID, REVIEWER_COMPANY_CODE, REVIEWER_USER_ID, POLICY_NO, CLAIM_NO, EXTERNAL_ESTIMATE_ID, SUPPLEMENT_NO,  CUSTOMER_NM, PROCESS_DT, APP_DETAIL, CREATED_DT, CREATED_BY, WORK_ITEM_TYPE, TRADING_PARTNER_ID, CLAIM_ID, CLAIM_EXPOSURE_ID, CORR_ID, MODULE_NM, APP_NM, SERVER_NM, STATUS,  CAST(d.FATAL_ERROR_COUNT AS VARCHAR2(30)) AS FATAL_ERROR_COUNT, CAST(d.NONFATAL_ERROR_COUNT AS VARCHAR2(30)) AS NONFATAL_ERROR_COUNT, CAST(d.WARNING_COUNT AS VARCHAR2(30)) AS WARNING_COUNT, e.SHORT_NAME, f.STATUS_TEXT from BILLADM.APP_LOG c, WORK_ITEM_STATUS d, TRANSACTIONTYPE e,TRANSACTIONSTATUS f where CLAIM_NO like '<>' and e.TRANSACTION_TYPE = c.TRANSACTION_TYPE and f.TRANSACTION_STATUS = c.STATUS and c.WORK_ITEM_ID = d.WORK_ITEM_ID(+)) ORDER BY PROCESS_DT) a ) ";
            Sql = Sql.Replace("<>", ClaimNumber);
            OracleConnection orcn = new OracleConnection("Data Source=billd.mitchell.com;User Id=CSTTUSER_DEV;Password=nkgg0ehjhnuug1");
            OracleCommand orcmd = new OracleCommand(Sql, orcn);
            DataSet ds = new DataSet();
            OracleDataAdapter adap = new OracleDataAdapter(orcmd);
            adap.Fill(ds);            
            BMData.nodeDataArray = new nodeData[ds.Tables[0].Rows.Count];
            BMData.linkDataArray = new linkData[ds.Tables[0].Rows.Count];
            //List<DesignerItemViewModelBase> itemList = new List<DesignerItemViewModelBase>();
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                //DesignerItemViewModelBase item;
                //if (dr["TRANSACTION_TYPE"].Equals("78201") || dr["TRANSACTION_TYPE"].Equals("20504") || dr["TRANSACTION_TYPE"].Equals("106900") || dr["TRANSACTION_TYPE"].Equals("101000") || dr["TRANSACTION_TYPE"].Equals("106908") || dr["TRANSACTION_TYPE"].Equals("79400"))
                {
                    //PersistDesignerItemViewModel itemT = new PersistDesignerItemViewModel();
                    //itemT.Parent = dvm;
                    //itemT.Left = left;
                    //itemT.Top = top;
                    //itemT.Event = dr["TRANSACTION_NAME"].ToString();
                    //itemT.ShortNameTip = dr["SHORT_NAME"].ToString();
                    node = dr["TRANSACTION_NAME"].ToString();
                    transaction = dr["SHORT_NAME"].ToString();
                    if (dr["STATUS_TEXT"].Equals("Processed with no errors"))
                    {
                        //itemT.Display = dr["SHORT_NAME"].ToString();//"Success";
                        //itemT.BackColor = "Green";
                    }
                    else
                    {
                        //itemT.Display = dr["SHORT_NAME"].ToString();//"Failure";//
                        //itemT.BackColor = "Red";
                        if (Convert.ToInt32(dr["FATAL_ERROR_COUNT"]) > 0)
                        {
                            //itemT.Display += ":" + dr["FATAL_ERROR_COUNT"];
                        }
                        if (Convert.ToInt32(dr["NONFATAL_ERROR_COUNT"]) > 0)
                        {
                            //itemT.Display += "" + dr["NONFATAL_ERROR_COUNT"];
                            //itemT.BackColor = "Blue";
                        }
                        if (Convert.ToInt32(dr["WARNING_COUNT"]) > 0)
                        {
                            //itemT.Display += "" + dr["WARNING_COUNT"];
                            //itemT.BackColor = "Yellow";
                        }
                    }
                    //itemT.DetailTip = "PROCESS_DT: " + dr["PROCESS_DT"] + "\n" + "CLAIM# " + dr["CLAIM_NO"] + "\n" + "STATUS: " + dr["STATUS_TEXT"];
                    //dvm.Items.Add(itemT);
                    //item = itemT;
                }
                //else
                //{
                //    //SettingsDesignerItemViewModel itemT = new SettingsDesignerItemViewModel();
                //    //itemT.Parent = dvm;
                //    //itemT.Left = left;
                //    //itemT.Top = top;
                //    ////item.HostUrl = dr["TRANSACTION_NAME"].ToString();
                //    //itemT.Event = dr["TRANSACTION_NAME"].ToString();
                //    //itemT.ShortNameTip = dr["SHORT_NAME"].ToString();
                //    if (dr["STATUS_TEXT"].Equals("Processed with no errors"))
                //    {
                //        //itemT.Display = dr["SHORT_NAME"].ToString(); //"Success";//
                //        //itemT.BackColor = "Green";
                //    }
                //    else
                //    {
                //        //itemT.Display = dr["SHORT_NAME"].ToString();//"Failure";//                                                               
                //        if (Convert.ToInt32(dr["NONFATAL_ERROR_COUNT"]) > 0)
                //        {
                //            //itemT.Display = dr["NONFATAL_ERROR_COUNT"] + ": " + itemT.Display;
                //            //itemT.BackColor = "Blue";
                //        }
                //        if (Convert.ToInt32(dr["WARNING_COUNT"]) > 0)
                //        {
                //            //itemT.Display = dr["WARNING_COUNT"] + ": " + itemT.Display;
                //            //itemT.BackColor = "Yellow";
                //        }
                //        if (Convert.ToInt32(dr["FATAL_ERROR_COUNT"]) > 0)
                //        {
                //            //itemT.Display = dr["FATAL_ERROR_COUNT"] + ": " + itemT.Display;
                //            //itemT.BackColor = "Red";
                //        }
                //    }
                //    //itemT.DetailTip = "PROCESS_DT: " + dr["PROCESS_DT"] + "\n" + "CLAIM# " + dr["CLAIM_NO"] + "\n" + "STATUS: " + dr["STATUS_TEXT"];
                //    //dvm.Items.Add(itemT);
                //    //item = itemT;
                //}               

                //itemList.Add(item);


                BMData.nodeDataArray[id] = new nodeData();
                BMData.nodeDataArray[id].id = id + 1;
                BMData.nodeDataArray[id].loc = left + " " + top;//"203 305";
                BMData.nodeDataArray[id].text = node;

                BMData.linkDataArray[id] = new linkData();
                if (id == 0)
                {
                    BMData.linkDataArray[id].from = 1;
                    BMData.linkDataArray[id].to = 1;
                    BMData.linkDataArray[id].text = "dummy";
                    //dummyBM.linkDataArray[0].curviness = 20;   
                }
                else
                {
                    //dummyBM.linkDataArray = new linkData[1];
                    //dummyBM.linkDataArray[id] = new linkData();
                    BMData.linkDataArray[id].from = id;
                    BMData.linkDataArray[id].to = id + 1;
                    BMData.linkDataArray[id].text = transaction;
                    //dummyBM.linkDataArray[0].curviness = 20;  
                    if(left==50)
                        BMData.linkDataArray[id].curviness = 20;
                }               

                left += 200+400;
                if (left > 900+1600)
                {
                    left = 50;
                    top += 200;                    
                }
                id++;
            }
            return BMData;
        }

        public List<BillModel> getDataList(string ClaimNumber)
        {
            List<BillModel> BMDataList = new List<BillModel>();
            int id=0;
            string transaction;
            string node;
            int left = 50, top = 50;
            string Sql = "select * from (select a.*, rownum rnum from ((select e.Transaction_NAME, e.TRANSACTION_TYPE, c.work_item_id, MITCHELL_COMPANY_CODE, MITCHELL_USER_ID, REVIEWER_COMPANY_CODE, REVIEWER_USER_ID, POLICY_NO, CLAIM_NO, EXTERNAL_ESTIMATE_ID, SUPPLEMENT_NO,  CUSTOMER_NM, PROCESS_DT, APP_DETAIL, CREATED_DT, CREATED_BY, WORK_ITEM_TYPE, TRADING_PARTNER_ID, CLAIM_ID, CLAIM_EXPOSURE_ID, CORR_ID, MODULE_NM, APP_NM, SERVER_NM, STATUS,  CAST(d.FATAL_ERROR_COUNT AS VARCHAR2(30)) AS FATAL_ERROR_COUNT, CAST(d.NONFATAL_ERROR_COUNT AS VARCHAR2(30)) AS NONFATAL_ERROR_COUNT, CAST(d.WARNING_COUNT AS VARCHAR2(30)) AS WARNING_COUNT, e.SHORT_NAME, f.STATUS_TEXT from BILLADM.APP_LOG c, WORK_ITEM_STATUS d, TRANSACTIONTYPE e,TRANSACTIONSTATUS f where CLAIM_NO like '<>' and e.TRANSACTION_TYPE = c.TRANSACTION_TYPE and f.TRANSACTION_STATUS = c.STATUS and c.WORK_ITEM_ID = d.WORK_ITEM_ID(+)) ORDER BY PROCESS_DT) a ) ";
            Sql = Sql.Replace("<>", ClaimNumber);
            OracleConnection orcn = new OracleConnection("Data Source=billd.mitchell.com;User Id=CSTTUSER_DEV;Password=nkgg0ehjhnuug1");
            OracleCommand orcmd = new OracleCommand(Sql, orcn);
            DataSet ds = new DataSet();
            OracleDataAdapter adap = new OracleDataAdapter(orcmd);
            adap.Fill(ds);            
            
            //List<DesignerItemViewModelBase> itemList = new List<DesignerItemViewModelBase>();
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                BillModel BMData = new BillModel();                
                BMData.nodeDataArray = new nodeData[ds.Tables[0].Rows.Count];
                BMData.linkDataArray = new linkData[ds.Tables[0].Rows.Count];
                node = dr["TRANSACTION_NAME"].ToString();
                transaction = dr["SHORT_NAME"].ToString();

                BMData.nodeDataArray[id] = new nodeData();
                BMData.nodeDataArray[id].id = id + 1;
                BMData.nodeDataArray[id].loc = left + " " + top;//"203 305";
                BMData.nodeDataArray[id].text = node;

                BMData.linkDataArray[id] = new linkData();
                if (id == 0)
                {
                    BMData.linkDataArray[id].from = 1;
                    BMData.linkDataArray[id].to = 1;
                    BMData.linkDataArray[id].text = "dummy";
                    //dummyBM.linkDataArray[0].curviness = 20;   
                }
                else
                {
                    //dummyBM.linkDataArray = new linkData[1];
                    //dummyBM.linkDataArray[id] = new linkData();
                    BMData.linkDataArray[id].from = id;
                    BMData.linkDataArray[id].to = id + 1;
                    BMData.linkDataArray[id].text = transaction;
                    //dummyBM.linkDataArray[0].curviness = 20;  
                    if (left == 50)
                        BMData.linkDataArray[id].curviness = 20;
                }

                left += 200 + 400;
                if (left > 900 + 1600)
                {
                    left = 50;
                    top += 200;
                }
                id++;
            }
            return null;
        }

        public BillModel getActivityDataProd(string ClaimNumber)
        {
            BillModel BMData = new BillModel();
            int id = 0;
            string transaction;
            string node;
            int left = 50, top = 50;
            string Sql = "select * from clm_activity_log where claim_id=(select claim_id from clm_claim where claim_number='<>')  order by created_dt";
            Sql = Sql.Replace("<>", ClaimNumber);
            OracleConnection orcn = new OracleConnection("Data Source=nmap.mitchell.com;User Id=sp102532;Password=sambhav8");
            OracleCommand orcmd = new OracleCommand(Sql, orcn);
            DataSet ds = new DataSet();
            OracleDataAdapter adap = new OracleDataAdapter(orcmd);
            adap.Fill(ds);
            BMData.nodeDataArray = new nodeData[ds.Tables[0].Rows.Count];
            BMData.linkDataArray = new linkData[ds.Tables[0].Rows.Count];

            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                node = dr["LOG_ENTRY"].ToString();
                transaction = dr["FIRST_NAME"].ToString() + "\n" + dr["LAST_NAME"].ToString();
                BMData.nodeDataArray[id] = new nodeData();
                BMData.nodeDataArray[id].id = id + 1;
                BMData.nodeDataArray[id].loc = left + " " + top;//"203 305";
                BMData.nodeDataArray[id].text = node;

                BMData.linkDataArray[id] = new linkData();
                if (id == 0)
                {
                    BMData.linkDataArray[id].from = 1;
                    BMData.linkDataArray[id].to = 1;
                    BMData.linkDataArray[id].text = "dummy";
                    //dummyBM.linkDataArray[0].curviness = 20;   
                }
                else
                {
                    //dummyBM.linkDataArray = new linkData[1];
                    //dummyBM.linkDataArray[id] = new linkData();
                    BMData.linkDataArray[id].from = id;
                    BMData.linkDataArray[id].to = id + 1;
                    BMData.linkDataArray[id].text = transaction;
                    //dummyBM.linkDataArray[0].curviness = 20;  
                    if (left == 50)
                        BMData.linkDataArray[id].curviness = 20;
                }

                left += 200 + 400;
                if (left > 900 + 1600)
                {
                    left = 50;
                    top += 200;
                }
                id++;
            }
            return BMData;
        }

        public BillModel getFilteredActivityData(string ClaimNumber, string[] keywords)
        {
            BillModel BMData = new BillModel();
            int id = 0;
            string transaction;
            string node;
            List<nodeData> nodeDataList = new List<nodeData>();
            List<linkData> linkDataList = new List<linkData>();
            bool added = false;
            long lastAddedDateKey=0;
            int left = 50, top = 50;
            DateTime CrDate = DateTime.Now;
            string Sql = "select * from clm_activity_log where claim_id=(select claim_id from clm_claim where claim_number='<>')  order by created_dt";
            Sql = Sql.Replace("<>", ClaimNumber);
            OracleConnection orcn = new OracleConnection("Data Source=nmad.mitchell.com;User Id=sp102532;Password=sambhav5");
            OracleCommand orcmd = new OracleCommand(Sql, orcn);
            DataSet ds = new DataSet();
            OracleDataAdapter adap = new OracleDataAdapter(orcmd);
            adap.Fill(ds);
            //BMData.nodeDataArray = new nodeData[ds.Tables[0].Rows.Count];
            //BMData.linkDataArray = new linkData[ds.Tables[0].Rows.Count];
            DataRow dr;
            //foreach (DataRow dr in ds.Tables[0].Rows)
            for(int i=0;i<ds.Tables[0].Rows.Count;i++)
            {
                added = false;
                dr = ds.Tables[0].Rows[i];
                if (id == 0)
                {                    
                    CrDate = DateTime.Parse(dr["CREATED_DT"].ToString()).AddSeconds(10);
                    id++;
                }
                //dr = ds.Tables[0].Rows[i];
                //node = dr["LOG_ENTRY"].ToString();
                //transaction = dr["FIRST_NAME"].ToString() + "\n" + dr["LAST_NAME"].ToString();

                //BMData.nodeDataArray[id] = new nodeData();
                //BMData.nodeDataArray[id].id = id + 1;
                //BMData.nodeDataArray[id].loc = left + " " + top;//"203 305";
                //BMData.nodeDataArray[id].text = node;

                //BMData.linkDataArray[id] = new linkData();
                //if (id == 0)
                //{
                //    //BMData.linkDataArray[id].from = 1;
                //    //BMData.linkDataArray[id].to = 1;
                //    //BMData.linkDataArray[id].text = "dummy";
                //    //dummyBM.linkDataArray[0].curviness = 20;   
                //    CrDate = DateTime.Parse(dr["CREATED_DT"].ToString()).AddSeconds(10);
                //    id++;
                //}
                //else
                //{
                //    //dummyBM.linkDataArray = new linkData[1];
                //    //dummyBM.linkDataArray[id] = new linkData();
                //    BMData.linkDataArray[id].from = id;
                //    BMData.linkDataArray[id].to = id + 1;
                //    BMData.linkDataArray[id].text = transaction;
                //    //dummyBM.linkDataArray[0].curviness = 20;  
                //    if (left == 50)
                //        BMData.linkDataArray[id].curviness = 20;
                //}

                //left += 200 + 400;
                //if (left > 900 + 1600)
                //{
                //    left = 50;
                //    top += 200;
                //}
                if (CrDate > DateTime.Parse(dr["CREATED_DT"].ToString()))
                {

                }
                else
                {
                    if (!lastAddedDateKey.Equals(DateTime.Parse(ds.Tables[0].Rows[i - 1]["CREATED_DT"].ToString()).ToFileTime()))
                    {
                        CrDate = DateTime.Parse(dr["CREATED_DT"].ToString()).AddSeconds(10);
                        dr = ds.Tables[0].Rows[i - 1];

                        node = dr["LOG_ENTRY"].ToString();
                        transaction = dr["FIRST_NAME"].ToString() + "\n" + dr["LAST_NAME"].ToString();

                        //BMData.nodeDataArray[id] = new nodeData();
                        //BMData.nodeDataArray[id].id = id + 1;
                        //BMData.nodeDataArray[id].loc = left + " " + top;//"203 305";
                        //BMData.nodeDataArray[id].text = node;
                        nodeData nd = new nodeData();
                        nd.id = id + 1;
                        nd.loc = left + " " + top;//"203 305";
                        nd.text = node;
                        nodeDataList.Add(nd);

                        //BMData.linkDataArray[id] = new linkData();
                        linkData ld = new linkData();
                        if (id == 0)
                        {
                            //BMData.linkDataArray[id].from = 1;
                            //BMData.linkDataArray[id].to = 1;
                            //BMData.linkDataArray[id].text = "dummy";
                            //dummyBM.linkDataArray[0].curviness = 20;   
                            CrDate = DateTime.Parse(dr["CREATED_DT"].ToString()).AddSeconds(10);
                            id++;
                        }
                        else
                        {
                            //dummyBM.linkDataArray = new linkData[1];
                            //dummyBM.linkDataArray[id] = new linkData();
                            //BMData.linkDataArray[id].from = id;
                            //BMData.linkDataArray[id].to = id + 1;
                            //BMData.linkDataArray[id].text = transaction;

                            ld.from = id;
                            ld.to = id + 1;
                            ld.text = transaction;
                            //dummyBM.linkDataArray[0].curviness = 20;  
                            //if (left == 50)
                                //BMData.linkDataArray[id].curviness = 20;
                                ld.curviness = 20;
                            linkDataList.Add(ld);
                        }

                        //left += 200 + 400;
                        left += 200 + 150;
                        top += 125;
                        if (left > 900 + 1600)
                        {
                            left = 400;
                            //top += 200;
                            top -= 200;
                        }


                        id++;
                        added = true;
                    }
                    //dtKey = DateTime.Parse(dr["CREATED_DT"].ToString()).ToFileTime();
                }
                //exception Case 1 --Last Row
                if (i == ds.Tables[0].Rows.Count - 1 && added==false)
                {
                    dr = ds.Tables[0].Rows[i];

                    node = dr["LOG_ENTRY"].ToString();
                    transaction = dr["FIRST_NAME"].ToString() + "\n" + dr["LAST_NAME"].ToString();                    
                    nodeData nd = new nodeData();
                    nd.id = id + 1;
                    nd.loc = left + " " + top;
                    nd.text = node;
                    nodeDataList.Add(nd);

                   
                    linkData ld = new linkData();
                    if (id == 0)
                    {                        
                        CrDate = DateTime.Parse(dr["CREATED_DT"].ToString()).AddSeconds(10);
                        id++;
                    }
                    else
                    {                        
                        ld.from = id;
                        ld.to = id + 1;
                        ld.text = transaction;                       
                       // if (left == 50)                           
                            ld.curviness = 20;
                        linkDataList.Add(ld);
                    }

                    //left += 200 + 400;
                    left += 200 + 150;
                    top += 125;
                    if (left > 900 + 1600)
                    {
                        left = 400;
                        //top += 200;
                        top -= 200;
                    }

                    id++;
                    added = true;
                    lastAddedDateKey = DateTime.Parse(dr["CREATED_DT"].ToString()).ToFileTime();
                }
                //exception Case 2 -- Keword
                foreach (string keyword in keywords)
                {
                    if (dr["LOG_ENTRY"].ToString().Contains(keyword) && added == false)
                    {
                        dr = ds.Tables[0].Rows[i];

                        node = dr["LOG_ENTRY"].ToString();
                        transaction = dr["FIRST_NAME"].ToString() + "\n" + dr["LAST_NAME"].ToString();
                        nodeData nd = new nodeData();
                        nd.id = id + 1;
                        nd.loc = left + " " + top;
                        nd.text = node;
                        nodeDataList.Add(nd);


                        linkData ld = new linkData();
                        if (id == 0)
                        {
                            CrDate = DateTime.Parse(dr["CREATED_DT"].ToString()).AddSeconds(10);
                            id++;
                        }
                        else
                        {
                            ld.from = id;
                            ld.to = id + 1;
                            ld.text = transaction;
                            //if (left == 50)
                                ld.curviness = 20;
                            linkDataList.Add(ld);
                        }

                        //left += 200 + 400;
                        left += 200 + 150;
                        top += 125;
                        if (left > 900 + 1600)
                        {
                            left = 400;
                            //top += 200;
                            top -= 200;
                        }

                        id++;
                        lastAddedDateKey = DateTime.Parse(dr["CREATED_DT"].ToString()).ToFileTime();
                    }
                }
            }
            BMData.nodeDataArray = nodeDataList.ToArray();
            BMData.linkDataArray = linkDataList.ToArray();
            return BMData;           
        }

        public BillModel getFilteredActivityDataProd(string ClaimNumber, string[] keywords)
        {
            BillModel BMData = new BillModel();
            int id = 0;
            string transaction;
            string node;
            List<nodeData> nodeDataList = new List<nodeData>();
            List<linkData> linkDataList = new List<linkData>();
            bool added = false;
            long lastAddedDateKey = 0;
            int left = 50, top = 50;
            DateTime CrDate = DateTime.Now;
            string Sql = "select * from clm_activity_log where claim_id=(select claim_id from clm_claim where claim_number='<>') order by created_dt";
            Sql = Sql.Replace("<>", ClaimNumber);
            OracleConnection orcn = new OracleConnection("Data Source=nmap.mitchell.com;User Id=sp102532;Password=sambhav8");
            OracleCommand orcmd = new OracleCommand(Sql, orcn);
            DataSet ds = new DataSet();
            OracleDataAdapter adap = new OracleDataAdapter(orcmd);
            adap.Fill(ds);            
            DataRow dr;            
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                added = false;
                dr = ds.Tables[0].Rows[i];
                if (id == 0)
                {
                    CrDate = DateTime.Parse(dr["CREATED_DT"].ToString()).AddSeconds(10);
                    id++;
                }                
                if (CrDate > DateTime.Parse(dr["CREATED_DT"].ToString()))
                {

                }
                else
                {
                    if (!lastAddedDateKey.Equals(DateTime.Parse(ds.Tables[0].Rows[i - 1]["CREATED_DT"].ToString()).ToFileTime()))
                    {
                        CrDate = DateTime.Parse(dr["CREATED_DT"].ToString()).AddSeconds(10);
                        dr = ds.Tables[0].Rows[i - 1];

                        node = dr["LOG_ENTRY"].ToString();
                        transaction = dr["FIRST_NAME"].ToString() + "\n" + dr["LAST_NAME"].ToString();                       
                        nodeData nd = new nodeData();
                        nd.id = id + 1;
                        nd.loc = left + " " + top;//"203 305";
                        nd.text = node;
                        nodeDataList.Add(nd);                        
                        linkData ld = new linkData();
                        if (id == 0)
                        {                           
                            CrDate = DateTime.Parse(dr["CREATED_DT"].ToString()).AddSeconds(10);
                            id++;
                        }
                        else
                        {                           
                            ld.from = id;
                            ld.to = id + 1;
                            ld.text = transaction;                           
                            ld.curviness = 20;
                            linkDataList.Add(ld);
                        }                     
                        left += 200 + 50;
                        top += 125;
                        if (left > 900 + 1600)
                        {
                            left = 300;                        
                            top -= 200;
                        }


                        id++;
                        added = true;
                    }                  
                }
                //exception Case 1 -- Last Entry
                if (i == ds.Tables[0].Rows.Count - 1 && added == false)
                {
                    dr = ds.Tables[0].Rows[i];

                    node = dr["LOG_ENTRY"].ToString();
                    transaction = dr["FIRST_NAME"].ToString() + "\n" + dr["LAST_NAME"].ToString();
                    nodeData nd = new nodeData();
                    nd.id = id + 1;
                    nd.loc = left + " " + top;
                    nd.text = node;
                    nodeDataList.Add(nd);


                    linkData ld = new linkData();
                    if (id == 0)
                    {
                        CrDate = DateTime.Parse(dr["CREATED_DT"].ToString()).AddSeconds(10);
                        id++;
                    }
                    else
                    {
                        ld.from = id;
                        ld.to = id + 1;
                        ld.text = transaction;                         
                        ld.curviness = 20;
                        linkDataList.Add(ld);
                    }                  
                    left += 200 + 50;
                    top += 125;
                    if (left > 900 + 1600)
                    {
                        left = 300;                   
                        top -= 200;
                    }

                    id++;
                    added = true;
                    lastAddedDateKey = DateTime.Parse(dr["CREATED_DT"].ToString()).ToFileTime();
                }
                //exception Case 2 -- Keword
                foreach (string keyword in keywords)
                {
                    if (dr["LOG_ENTRY"].ToString().Contains(keyword) && added == false)
                    {
                        dr = ds.Tables[0].Rows[i];

                        node = dr["LOG_ENTRY"].ToString();
                        transaction = dr["FIRST_NAME"].ToString() + "\n" + dr["LAST_NAME"].ToString();
                        nodeData nd = new nodeData();
                        nd.id = id + 1;
                        nd.loc = left + " " + top;
                        nd.text = node;
                        nodeDataList.Add(nd);


                        linkData ld = new linkData();
                        if (id == 0)
                        {
                            CrDate = DateTime.Parse(dr["CREATED_DT"].ToString()).AddSeconds(10);
                            id++;
                        }
                        else
                        {
                            ld.from = id;
                            ld.to = id + 1;
                            ld.text = transaction;                           
                            ld.curviness = 20;
                            linkDataList.Add(ld);
                        }                       
                        left += 200 + 50;
                        top += 125;
                        if (left > 900 + 1600)
                        {
                            left = 300;                       
                            top -= 200;
                        }

                        id++;
                        lastAddedDateKey = DateTime.Parse(dr["CREATED_DT"].ToString()).ToFileTime();
                    }
                }
            }
            BMData.nodeDataArray = nodeDataList.ToArray();
            BMData.linkDataArray = linkDataList.ToArray();
            return BMData;
        }

        static public BillModel returnDummy()
        {
            BillModel dummyBM = new BillModel();
            dummyBM.nodeDataArray = new nodeData[1];
            dummyBM.nodeDataArray[0] = new nodeData();
            dummyBM.nodeDataArray[0].id = 1;
            dummyBM.nodeDataArray[0].loc = "203 305";
            dummyBM.nodeDataArray[0].text = "dummy";

            dummyBM.linkDataArray = new linkData[1];
            dummyBM.linkDataArray[0] = new linkData();
            dummyBM.linkDataArray[0].from = 1;
            dummyBM.linkDataArray[0].to = 1;
            dummyBM.linkDataArray[0].text = "dummy";
            dummyBM.linkDataArray[0].curviness = 20;

            return dummyBM;
        }
    }

    public class nodeData
    {
        public int id;
        public string loc;
        public string text;

        //public nodeData(int id, string loc, string text)
        //{
        //    this.id = id;
        //    this.loc = loc;
        //    this.text = text;
        //}
    }
    public class linkData
    {
        public int from;
        public int to;
        public string text;
        public int curviness;
    }
}