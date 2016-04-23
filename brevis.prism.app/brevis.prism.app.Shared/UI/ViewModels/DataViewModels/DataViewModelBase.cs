using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Practices.Prism.Mvvm;

namespace brevis.prism.app.UI.ViewModels.DataViewModels
{
    public class DataViewModelBase : ViewModel
    {
        private int _id;
        private string _objectId;
        private string _header;
        private string _subHeader;
        private DateTime _timestamp;
        private List<DataViewModelBase> _dataItems;

        public int Id
        {
            get { return _id; }
            set { SetProperty(ref _id, value); }
        }
        public string ObjectId
        {
            get { return _objectId; }
            set { SetProperty(ref _objectId, value); }
        }
        public DateTime Timestamp
        {
            get { return _timestamp; }
            set { SetProperty(ref _timestamp, value); }
        }
        public string Header
        {
            get { return _header; }
            set { SetProperty(ref _header, value); }
        }
        public string SubHeader
        {
            get { return _subHeader; }
            set { SetProperty(ref _subHeader, value); }
        }

        public List<DataViewModelBase> DataItems
        {
            get { return _dataItems; }
            set { SetProperty(ref _dataItems, value); }
        }

        public DataViewModelBase()
        {
            DataItems = new List<DataViewModelBase>();
        }
    }
}
