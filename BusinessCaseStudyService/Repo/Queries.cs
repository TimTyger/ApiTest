using BusinessCaseStudyService.Models;
using BusinessCaseStudyService.Models.Script;
using BusinessCaseStudyService.Utilities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BusinessCaseStudyService.Repo
{
    public static class Queries
    {
        public async static Task<QueryHandler> LogPostedTxn(TxnModel txnModel)
        {
            var script = string.Empty;
            var defaultDate = DateTime.Now.ToString("dd-MM-yyyy HH:mm:ss");
            try
            {
                var parameters = new
                {
                    ttdebitaccount = txnModel.DebitAccount,
                    ttcreditaccount = txnModel.CreditAccount,
                    ttREF_AMOUNT = txnModel.RefAmount,
                    ttPOST_AMT = txnModel.PostAmount?? txnModel.RefAmount,
                    ttNARRATION = txnModel.Narration,
                    ttCURRENCYCODE = txnModel.CurrencyCode.Length>3? txnModel.CurrencyCode.Substring(0,3): txnModel.CurrencyCode,
                    ttSTATUS = string.IsNullOrWhiteSpace(txnModel.Status) ? "U" : txnModel.Status,
                    ttREFNO = txnModel.RefNo,
                    ttREASON = txnModel.Reason??txnModel.Narration,
                    ttRESPONSECODE = txnModel.ResponseCode,
                    ttDEPOSITORNAME = string.IsNullOrWhiteSpace(txnModel.Depositor) ? "NA" : txnModel.Depositor,
                    ttDEPOSITORMOBILE = string.IsNullOrWhiteSpace(txnModel.DepositorMobile) ? "NA" : txnModel.DepositorMobile,
                    ttDEPOSITORADDRESS = string.IsNullOrWhiteSpace(txnModel.DepositorAddress) ? "NA" : txnModel.DepositorAddress,
                    ttPROCESSCODE = string.IsNullOrWhiteSpace(txnModel.ProcessCode) ? "NA" : txnModel.ProcessCode,
                    ttCHARGE = txnModel.Charge,
                    ttVAT = txnModel.Vat ?? "1",
                    ttCURRENCYCODE_FROM = txnModel.CurrencyCode,
                    ttRATE_FROM = txnModel.RateFrom ?? "1",
                    ttRATE_TO = txnModel.RateTo ?? "1",
                    ttDEBIT_BRANCH = txnModel.DebitBranch,
                    ttCREDIT_BRANCH = txnModel.CreditBranch,
                    ttREVERSAL_STAT = string.IsNullOrWhiteSpace(txnModel.RevStatus) ? "N" : txnModel.RevStatus,
                    ttPOST_DATE = string.IsNullOrWhiteSpace(txnModel.PostDate) ? defaultDate : await Helpers.toSystemDate(txnModel.PostDate),
                    ttTXN_DATE = string.IsNullOrWhiteSpace(txnModel.TxnDate) ? defaultDate : await Helpers.toSystemDate(txnModel.TxnDate),
                    ttTRAN_ID = txnModel.TranId??await Helpers.GenerateRandom(15),
                    ttTTTXNTYPE = txnModel.TxnType,
                    ttTTBENENAME = txnModel.BeneficiaryName,
                    ttTTDestBank = txnModel.DestBank,
                    ttRESPONSE_MSG = txnModel.ResponseMessage,
                    ttCOUNTRY_CODE = txnModel.CountryCode?.Length > 3 ? txnModel.CountryCode.Substring(0, 3) : txnModel.CountryCode
                };

                script = @"INSERT INTO 
                POSTEDTXN
                    ( DEBITACCOUNT, CREDITACCOUNT,  REF_AMOUNT, POST_AMT,NARRATION, CURRENCYCODE, STATUS, REFNO,  REASON, RESPONSECODE, DEPOSITORNAME, DEPOSITORMOBILE, DEPOSITORADDRESS, PROCESSCODE,
                    CHARGE, VAT, CURRENCYCODE_FROM, RATE_FROM, RATE_TO, 
                    DEBIT_BRANCH, CREDIT_BRANCH, REVERSAL_STAT,POST_DATE,TXN_DATE, TRAN_ID, TTTXNTYPE,
                    TTBENENAME, DEST_BANK,RESPONSE_MSG,COUNTRY_CODE
                    
                    )                        
                VALUES
                    (
                    @ttdebitaccount, @ttcreditaccount, @ttREF_AMOUNT,@ttPOST_AMT, @ttNARRATION, @ttCURRENCYCODE,  @ttSTATUS, @ttREFNO, @ttREASON, @ttRESPONSECODE, @ttDEPOSITORNAME, @ttDEPOSITORMOBILE, @ttDEPOSITORADDRESS, @ttPROCESSCODE,
                     @ttCHARGE, @ttVAT,  @ttCURRENCYCODE_FROM, @ttRATE_FROM, 
                    @ttRATE_TO, @ttDEBIT_BRANCH, @ttCREDIT_BRANCH, @ttREVERSAL_STAT, @ttPOST_DATE,  @ttTXN_DATE, @ttTRAN_ID,@ttTTTXNTYPE, @ttTTBENENAME, @ttTTDestBank,
                    @ttRESPONSE_MSG,@ttCOUNTRY_CODE
                    )";

                return (new QueryHandler { Script = script, Parameters = parameters, status = true });

            }
            catch (Exception)
            {
                //nLogger.Error($"Generate PostedTxn Script Exception Message: {ex.Message.ToString()}\r\n");
                return (new QueryHandler { status = false });
            }
        }


        public static Task<QueryHandler> GetTxnStatus(string refNo)
        {
            var results = new QueryHandler();
            try
            {
                var parameters = new
                {
                    tRefNo = refNo
                };

                var script = $@"SELECT STATUS as Status FROM POSTEDTXN 
                                WHERE REFNO = @tRefNo ";

                results = new QueryHandler { status = true, Script = script, Parameters = parameters };
            }
            catch (Exception ex)
            {
                throw;
            }
            return Task.FromResult(results);
        }
    }
}
