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
    
    public partial class Grade
    {
        public int GradeID { get; set; }
        public Nullable<int> UserIDGrade { get; set; }
        public Nullable<int> SubjectIDGrade { get; set; }
        public Nullable<int> GradeValue { get; set; }
    
        public virtual Subject Subject { get; set; }
        public virtual Users Users { get; set; }
    }
}
