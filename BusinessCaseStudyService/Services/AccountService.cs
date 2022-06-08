using BusinessCaseStudyService.Models;
using BusinessCaseStudyService.Repo;
using BusinessCaseStudyService.Utilities;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace BusinessCaseStudyService.Services
{
    public class AccountService : ControllerBase, IAccountService
    {
        private readonly IAccountRepo _accountRepo;

        public AccountService(IAccountRepo accountRepo)
        {
            _accountRepo = accountRepo;
        }
        public async Task<IActionResult> CreateTransactionRequest(TransactionRequest request, string route)
        {
            var logId = $"{Guid.NewGuid()}"; var resp = new ResponseObject<string>();

            try
            {
                if (!ModelState.IsValid)
                {
                    var invalidRes = new ErrorResponse
                    {
                        Message = $"{ConstMessage.INVALID_REQ_OBJ}",
                        ProcessId = logId
                    };
                    return StatusCode(400, invalidRes);
                }
                if (request.ProcessCode!="00" && request.ProcessCode != "01")
                {
                    var invalidRes = new ErrorResponse
                    {
                        Message = $"{ConstMessage.INVALID_REQ_OBJ}",
                        ProcessId = logId
                    };
                    return StatusCode(400, invalidRes);
                }
                var model = new TxnModel
                {
                    DestBank = request.DestBank,
                    RefAmount = request.Amount,
                    BeneficiaryName = request.BeneficiaryName,
                    Charge = request.BeneficiaryName,
                    CreditAccount = request.CreditAccount,
                    CurrencyCode = request.CurrencyCode,
                    Vat = request.Vat,
                    DepositorMobile = request.DepositorMobile ?? "NA",
                    Depositor = request.Depositor,
                    DepositorAddress = request.DepositorAddress ?? "NA",
                    Narration = request.Narration,
                    DebitAccount = request.DebitAccount,
                    DebitBranch = "NA",
                    CreditBranch = "NA",
                    CountryCode = request.CountryCode,
                    TxnType = GetTranType(request.ProcessCode),
                    ProcessCode = request.ProcessCode,
                    RefNo =$"{DateTime.UtcNow.ToString("yyyyMMddHHmmss")}{await Helpers.GenerateRandom(6)}",
                    
                };
                var result = await _accountRepo.LogPostedTxnAsync(model,logId);
                resp = new ResponseObject<string>
                {
                    Data = result == true ? $"{ConstMessage.SUCCESS}" : $"{ConstMessage.FAIL}",
                    Message = result == true ? $"{ConstMessage.SUCCESS}" : $"{ConstMessage.FAIL}",
                    StatusCode = result == true ? "200" : "500",
                    StatusMessage = result == true ? $"{ConstMessage.SUCCESS}" : $"{ConstMessage.FAIL}",
                };
            }
            catch (Exception)
            {

                throw;
            }
            return Ok(resp);

        }
        public async Task<IActionResult> CheckTransactionStatus(StatusReq request, string route)
        {
            var logId = $"{Guid.NewGuid()}"; var resp = new ResponseObject<StatusCheckerRes>();
            try
            {
                if (!ModelState.IsValid)
                {
                    var invalidRes = new ErrorResponse
                    {
                        Message = $"{ConstMessage.INVALID_REQ_OBJ}",
                        ProcessId = logId
                    };
                    return StatusCode(400, invalidRes);
                };
                var result = await _accountRepo.GetTxnStatusAsync(request.TransactionRefId,logId);
                resp = new ResponseObject<StatusCheckerRes>
                {
                    Data = result,
                    Message = result !=null? $"{ConstMessage.SUCCESS}" : $"{ConstMessage.FAIL}",
                    StatusCode = result != null ? "200" : "500",
                    StatusMessage = result != null ? $"{ConstMessage.SUCCESS}" : $"{ConstMessage.FAIL}",
                };
            }
            catch (Exception)
            {

                throw;
            }
            return Ok(resp);

        }
    
        private string GetTranType(string code)
        {
            string output;
            switch (code)
            {
                case "00":
                    output = "trx";break;
                default:
                    output= "bll";break;
            }
            return output;
        }
    }
    public interface IAccountService
    {
        Task<IActionResult> CreateTransactionRequest(TransactionRequest request, string route);
        Task<IActionResult> CheckTransactionStatus(StatusReq request, string route);
    }
}
