using System;
using System.Threading.Tasks;
using Piller.Data;
using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using SQLite;
using MvvmCross.Platform;
using MvvmCross.Plugins.File;

namespace Piller.Services
{
    public class MedicinesDatabase : IMedicineDatabase
    {
        private readonly IMvxFileStore fileStore = Mvx.Resolve<IMvxFileStore>();

        private readonly string databaseFileName = "leki.db";
        private SQLiteAsyncConnection connection;

        public MedicinesDatabase()
        {
            this.connection = new SQLiteAsyncConnection(this.fileStore.NativePath("Piller") + databaseFileName);

            this.connection.GetConnection().CreateTable<Medicines>();
        }

        public async Task<T> GetAsync<T>(int KodEAN) where T : new()
        {
            return await this.connection.FindAsync<T>(KodEAN);
        }
    }
}
