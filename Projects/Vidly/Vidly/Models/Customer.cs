using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Vidly.Models
{
    public class Customer
    {

        public int Id { get; set; }

        [Required]
        [StringLength(255)]
        public string Name { get; set; }

        public bool IsSubscribedToNewsletter { get; set; }

        
        public MembershipType MembershipType { get; set; } //Navigation property, permite a gente navegar de um tipo de classe para outro, nesse caso de Customer para MembershipType


        [Display(Name = "Membership Type")]
        public byte MembershipTypeId { get; set; } //Trazemos a chave estrangeira para optimizar a aplicação

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Display(Name = "Date of Birth")] //Forma de mostrar o seu label no HTML
        [Min18YearsIfAMember]
        public DateTime? BirthDate { get; set; }
        


    }
}