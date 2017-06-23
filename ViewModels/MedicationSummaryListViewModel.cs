﻿using System;
using System.Reactive;
using MvvmCross.Core.ViewModels;
using ReactiveUI;
using System.Collections.Generic;
using Piller.Data;
using Piller.Services;
using MvvmCross.Platform;
using System.Threading.Tasks;
using MvvmCross.Plugins.Messenger;
using System.Linq;

namespace Piller.ViewModels
{
	public class MedicationSummaryListViewModel : MvxViewModel
	{
		private IPermanentStorageService storage = Mvx.Resolve<IPermanentStorageService>();
        MvxSubscriptionToken dataChangedSubscriptionToken;

		private List<MedicationDosage> medicationList;
		public List<MedicationDosage> MedicationList
		{
			get { return medicationList; }
			set { SetProperty(ref medicationList, value);RaisePropertyChanged(nameof(IsEmpty)); }
		}
        public bool IsEmpty
        {
            get { return !medicationList.Any(); }
        }

        public ReactiveCommand<Unit, bool> AddNew { get; }
		public ReactiveCommand<Data.MedicationDosage, Unit> Edit { get; }

		public MedicationSummaryListViewModel()
		{
			AddNew = ReactiveCommand.Create(() => this.ShowViewModel<MedicationDosageViewModel>());
			Edit = ReactiveCommand.Create<Data.MedicationDosage>((item) =>
			 {
                this.ShowViewModel<MedicationDosageViewModel>(new MedicationDosageNavigation { MedicationDosageId = item.Id.Value });
			 });

            dataChangedSubscriptionToken = Mvx.Resolve<IMvxMessenger>().Subscribe<DataChangedMessage>(async mesg => await Init());
		}

        public async Task Init()
        {
	
			var items = await storage.List<MedicationDosage>();
            if (items != null)
                MedicationList = items;
            else
                MedicationList = new List<MedicationDosage>();
        }
	
		
	}
}