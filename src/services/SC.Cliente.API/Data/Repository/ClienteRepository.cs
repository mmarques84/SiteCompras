using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SC.Clientes.API.Models;

using SC.Core.Data;

namespace SC.Clientes.API.Data.Repository
{
    public class ClienteRepository : IClienteRepository
    {
        private readonly ClientesContext _context;

        public ClienteRepository(ClientesContext context)
        {
            _context = context;
        }

        public IUnitOfWork UnitOfWork => _context;

        public async Task<IEnumerable<Cliente>> ObterTodos()
        {
            return await _context.GetClientes().AsNoTracking().ToListAsync();
        }

        public Task<Cliente> ObterPorCpf(string cpf)
        {
            return _context.GetClientes().FirstOrDefaultAsync(c => c.Cpf.Numero == cpf);
        }

        public void Adicionar(Cliente cliente)
        {
            _context.GetClientes().Add(cliente);
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}