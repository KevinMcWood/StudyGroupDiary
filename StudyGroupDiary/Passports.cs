//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан по шаблону.
//
//     Изменения, вносимые в этот файл вручную, могут привести к непредвиденной работе приложения.
//     Изменения, вносимые в этот файл вручную, будут перезаписаны при повторном создании кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace StudyGroupDiary
{
    using System;
    using System.Collections.Generic;
    
    public partial class Passports
    {
        public int UIDP { get; set; }
        public int Number { get; set; }
        public string TypeOfDocument { get; set; }
        public int Series { get; set; }
        public string IssuedByWhom { get; set; }
        public string Citizenship { get; set; }
        public System.DateTime DateOfIssue { get; set; }
        public string Nationality { get; set; }
    
        public virtual Users Users { get; set; }
    }
}
