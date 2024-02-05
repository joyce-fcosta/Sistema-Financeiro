using Domain.Interfaces.Generics;
using Infra.Configuration;
using Microsoft.EntityFrameworkCore;
using Microsoft.Win32.SafeHandles;
using System.Runtime.InteropServices;

namespace Infra.Repository.Generics
{
    public class GenericsRepository<T> : IGenerics<T>, IDisposable where T : class
    {
        private readonly DbContextOptions<ApplicationContext> _options;
        public GenericsRepository()
        {
            _options = new DbContextOptions<ApplicationContext>();

        }
        public async Task Add(T objeto)
        {
            using (var data = new ApplicationContext(_options))
            {
                await data.Set<T>().AddAsync(objeto);
                await data.SaveChangesAsync();
            }
        }

        public async Task Delete(T objeto)
        {
            using (var data = new ApplicationContext(_options))
            {
                data.Set<T>().Remove(objeto);
                await data.SaveChangesAsync();
            }
        }

        public async Task<T> GetEntityById(int id)
        {
            using (var data = new ApplicationContext(_options))
            {
                return await data.Set<T>().FindAsync(id);
            }
        }

        public async Task<List<T>> List()
        {
            using (var data = new ApplicationContext(_options))
            {
                return await data.Set<T>().ToListAsync();
            }
        }

        public async Task Uppdate(T objeto)
        {
            using (var data = new ApplicationContext(_options))
            {
                data.Set<T>().Update(objeto);
                await data.SaveChangesAsync();
            }
        }

        // To detect redundant calls
        private bool _disposedValue = false;

        // Instantiate a SafeHandle instance.
        private SafeHandle? _safeHandle = new SafeFileHandle(IntPtr.Zero, true);

        // Public implementation of Dispose pattern callable by consumers.
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        // Protected implementation of Dispose pattern.
        protected virtual void Dispose(bool disposing)
        {
            if (!_disposedValue)
            {
                if (disposing)
                {
                    _safeHandle?.Dispose();
                    _safeHandle = null;
                }

                _disposedValue = true;
            }
        }
    }
}
