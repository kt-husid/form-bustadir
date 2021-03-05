using BustadirForm.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace BustadirForm.Interfaces
{
    public interface IViewModelBase
    {
        int Id { get; set; }
    }

    public abstract class ViewModelBase : IViewModelBase
    {
        public int Id { get; set; }
    }

    public abstract class ViewModelBaseWithChangeEvent : IViewModelBase
    {
        public int Id { get; set; }
    }

    public interface IHasId
    {
        int Id { get; set; }
    }

    public interface IHasEntity : IHasId
    {
        int? EntityId { get; set; }
        Entity Entity { get; set; }
    }
}
