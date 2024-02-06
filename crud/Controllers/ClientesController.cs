using crud.Models;
using crud.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace crud.Controllers
{
    public class ClientesController : Controller
    {
        private readonly MVCDemoDbContext _context;
        public ClientesController(MVCDemoDbContext mvcDemoDbContext)
        {
            this._context = mvcDemoDbContext;
        }

        [HttpGet]

        public async Task<IActionResult> Index()
        {
            var clientes = await _context.Clientes.ToListAsync();
            return View(clientes);
        }

        [HttpGet]
        public IActionResult Add()
        {
            return View(); 
        }

        [HttpPost]
        public async Task<IActionResult> Add(AddClienteViewModel addClienteResquest)
        {
            var cliente = new Cliente()
            {
                Id = Guid.NewGuid(),
                Nome = addClienteResquest.Nome,
                cpf = addClienteResquest.cpf,
                Telefone = addClienteResquest.Telefone,
                DataNascimento = addClienteResquest.DataNascimento
            };

            await _context.Clientes.AddAsync(cliente);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> View(Guid id)
        {
            var cliente = await _context.Clientes.FirstOrDefaultAsync(x => x.Id == id);

            if (cliente != null)
            {
                var viewModel = new UpdateClienteViewModel()
                {
                    Id = cliente.Id,
                    Nome = cliente.Nome,
                    cpf = cliente.cpf,
                    Telefone = cliente.Telefone,
                    DataNascimento = cliente.DataNascimento
                };

                return await Task.Run(() => View("View", viewModel));
            }

            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> View(UpdateClienteViewModel model)
        {
            var cliente = await _context.Clientes.FindAsync(model.Id);

            if (cliente != null)
            {
                cliente.Nome = model.Nome;
                cliente.Telefone = model.Telefone;
                cliente.cpf = model.cpf;
                cliente.DataNascimento = model.DataNascimento;

                await _context.SaveChangesAsync();

                return RedirectToAction("Index");
            }

            return RedirectToAction("Index");

        }

        [HttpPost]
        public async Task<IActionResult> Delete(UpdateClienteViewModel model)
        {
            var cliente = await _context.Clientes.FindAsync(model.Id);

            if(cliente != null)
            {
                _context.Clientes.Remove(cliente);
                await _context.SaveChangesAsync();

                return RedirectToAction("Index");
            }

            return RedirectToAction("Index");

        }
    }
}
