using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ListViewTest.Models
{
    public class Student : BindableBase
    {
        //public int Id { get; set; }
        //public string Name { get; set; }

        #region Id
        private int _Id;
        /// <summary>
        /// 學生代號
        /// </summary>
        public int Id
        {
            get { return this._Id; }
            set { this.SetProperty(ref this._Id, value); }
        }
        #endregion


        #region Name
        private string _Name;
        /// <summary>
        /// 學生姓名
        /// </summary>
        public string Name
        {
            get { return this._Name; }
            set { this.SetProperty(ref this._Name, value); }
        }
        #endregion
    }
}
