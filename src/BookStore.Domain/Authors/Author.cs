using JetBrains.Annotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Domain.Entities.Auditing;

namespace BookStore.Authors
{
    /// <summary>
    /// https://docs.abp.io/en/abp/latest/Tutorials/Part-6?UI=MVC&DB=EF
    /// The Author Entity
    /// 
    /// Inherited from FullAuditedAggregateRoot<Guid> which makes the entity soft delete
    /// (that means when you delete it, it is not deleted in the database, but just marked as deleted) with all the auditing(审计) properties
    /// 
    /// private set for the Name property restricts to set this property from out of this class. 
    /// There are two ways of setting the name (in both cases, we validate the name): 
    /// In the constructor, while creating a new author.
    /// Using the ChangeName method to update the name later.
    /// 
    /// The constructor and the ChangeName method is internal to force to use these methods only in the domain layer, 
    /// using the AuthorManager that will be explained later. 
    /// 
    /// Check class is an ABP Framework utility class to help you while checking method arguments 
    /// (it throws ArgumentException on an invalid case).
    /// </summary>
    public class Author : FullAuditedAggregateRoot<Guid>
    {
        public string Name { get; private set; }
        public DateTime BirthDate { get; set; }
        public string ShortBio { get; set; }

        private Author()
        {
            /* This constructor is for deserialization / ORM purpose */
        }

        internal Author(Guid id, [NotNull] string name, DateTime birthDate, [CanBeNull] string shortBio = null) : base(id)
        {
            SetName(name);
            BirthDate = birthDate;
            ShortBio = shortBio;
        }

        internal Author ChangeName([NotNull] string name)
        {
            SetName(name);
            return this;
        }

        private void SetName([NotNull] string name)
        {
            Name = Check.NotNullOrWhiteSpace(name, nameof(name), maxLength: AuthorConsts.MaxNameLength);
        }
    }
}
