using CarRental.Data;
using CarRental.Models;
using CarRental.Repositories.Interfaces;

namespace CarRental.Repositories.Implementations
{
    public class RequestRepository : IRequestRepository
    {
        private readonly ApplicationDbContext _context;

        public RequestRepository(ApplicationDbContext context) 
        {
            _context = context;
        }
        public void Add(Request request)
        {
            _context.Requests.Add(request);
            _context.SaveChanges();
        }
        IEnumerable<Request> IRequestRepository.GetAll()
        {
            var requests = _context.Requests.
                         Where(b => !b.IsRejected)
                         .ToList();

            return requests;
        }
    }
}
