using infrastructure_mojo.Interfaces;
using Microsoft.EntityFrameworkCore;
using core_mojo.models;
using core_mojo.Dtos;
using core_mojo;

namespace infrastructure_mojo.repositories
{
    public class DiscussionRepository : Repository<Discussion>, IDiscussion
    {
        public DiscussionRepository(AppDbContext context) : base(context) {}

    }
}