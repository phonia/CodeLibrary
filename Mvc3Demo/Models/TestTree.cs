namespace Mvc3Demo.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("TestTree")]
    public partial class TestTree
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public TestTree()
        {
            TestTree1 = new HashSet<TestTree>();
        }

        public Guid Id { get; set; }

        [StringLength(10)]
        public string name { get; set; }

        public Guid? Parent { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<TestTree> TestTree1 { get; set; }

        public virtual TestTree TestTree2 { get; set; }
    }

    public partial class TestTree
    {
        [NotMapped]
        public string state { get; set; }

        public void Init()
        {
            state = "closed";
        }
    }
}
