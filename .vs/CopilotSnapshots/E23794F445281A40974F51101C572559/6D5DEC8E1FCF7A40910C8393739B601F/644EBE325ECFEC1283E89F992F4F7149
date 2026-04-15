using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SmartBank.TransactionService.DTOs;
using SmartBank.TransactionService.Services;

namespace SmartBank.TransactionService.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class TransactionsController : ControllerBase
    {
        private ITransactionService _service;

        public TransactionsController(ITransactionService service)
        {
            _service = service;
        }

        [HttpGet("account/{accountId}")]
        public async Task<IActionResult> GetAll(int accountId)
        {
            var transactions = await _service.GetTransactions(accountId);
            return Ok(transactions);
        }

        //[HttpGet("{id}")]
        //public async Task<IActionResult> GetById(int id)
        //{
        //    try
        //    {
        //        var transaction = await _service.GetTransactionbyId(id);
        //        return Ok(transaction);
        //    }
        //    catch (Exception e)
        //    {
        //        return NotFound(e.Message);
        //    }
        //}

        [HttpPost]
        public async Task<IActionResult> Create(CreateTransactionDto createTransactionDto)
        {
            try
            {
                var transaction = await _service.CreateTransaction(createTransactionDto);
                return Ok(transaction); 
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}
