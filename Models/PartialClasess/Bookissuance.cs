﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Library.Models
{
    public partial class Bookissuance
    {
        public Visibility VisibilityBtn
        {
            get
            {
                if (App.AuthUser.RoleId == 1)
                    return Visibility.Visible;
                else
                    return Visibility.Collapsed;

            }
        }
        public string OverdueDate
        {
            get
            {
                if(DateOfReturn < DateTime.Now)
                {
                    return "Возврат просрочен";
                }
                else
                {
                    return "";
                }

            }
        }
        public string ColorOverdueDate
        {
            get
            {
                if (DateOfReturn < DateTime.Now)
                    return "#ff0000";
                else
                    return "#d3d3d3";
            }
        }

    }
}
