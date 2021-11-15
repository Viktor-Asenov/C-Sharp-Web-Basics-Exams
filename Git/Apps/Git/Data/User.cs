using SUS.MvcFramework;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Git.Data
{
    public class User : IdentityUser<string>
    {
        public User()
        {
            this.Id = Guid.NewGuid().ToString();
            this.Repositories = new HashSet<Repository>();
            this.Commits = new HashSet<Commit>();
        }

        public virtual ICollection<Repository> Repositories { get; set; }

        public virtual ICollection<Commit> Commits { get; set; }
    }
}
