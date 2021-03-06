﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FlightReservationSystem
{
    public partial class SelectAirplanePage : DisplayAirplanesPage
    {
        public SelectAirplanePage(ReservationSystem reservationSystem)
            : base(reservationSystem)
        {
            InitializeComponent();
        }

        public void SetIIDSelectionCallBack(IDSelectionCallBack c)
        {
            idSelectionCallBack = c;
        }

        protected void OnRowHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            Guid ID = Guid.Parse(PlanesGridView.Rows[e.RowIndex].Cells["ID"].Value.ToString());
            if (idSelectionCallBack != null)
                this.idSelectionCallBack(ID);
            this.Close();

        }


        private IDSelectionCallBack idSelectionCallBack = null;
    }
}
