using CarRental.Data;
using CarRental.Models;
using CarRental.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

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
                         Where(r => !r.IsRejected && !r.IsAccepted)
                         .Include(r => r.Car)
                         .Include(r => r.User)
                         .ToList();

            return requests;
        }
        public void Update(Request request)
        {
            _context.Requests.Update(request);
            _context.SaveChanges();
        }

        public Request GetRequestByID(int id)
        {
            return _context.Requests.Find(id);
        }
    }
}
