#region Copyright
// **********************************************************************
// Copyright (C) 2022 HeJing
//
// Script Name :		LearnA.cs
// Author Name :		YunShu
// Create Time :		2022/05/17 14:37:40
// Description :           c# ????
// **********************************************************************
#endregion


using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LJVoyage.Game.Test
{
    public class LearnA : Attribute
    {
        public LearnA(String Descrition_in)
        {
            this.description = Descrition_in;
        }
        protected String description;
        public String Description
        {
            get
            {
                return this.description;
            }
        }
    }
}