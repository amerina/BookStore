using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Entities.Auditing;

namespace BookStore.Books
{
    public class Book : AuditedAggregateRoot<Guid>
    {
        /// <summary>
        /// In this tutorial, we preferred to not add a navigation property to the Author entity from the Book class 
        /// (like public Author Author { get; set; }). 
        /// This is due to follow the DDD best practices (rule: refer to other aggregates only by id). 
        /// However, you can add such a navigation property and configure it for the EF Core. 
        /// In this way, you don't need to write join queries while getting books with their authors (like we will done below) 
        /// which makes your application code simpler.
        /// </summary>
        public Guid AuthorId { get; set; }
        public string Name { get; set; }

        public BookType Type { get; set; }

        public DateTime PublishDate { get; set; }

        public float Price { get; set; }
    }
}
