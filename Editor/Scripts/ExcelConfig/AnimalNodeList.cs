using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LJVoyage.GameEditor
{
    [System.Serializable]
    public class AnimalNodeList
    {
        /// <summary>
        /// 动物类型
        /// </summary>
        public string animalType;
        private string AnimalType
        {
            get { return animalType; }
            set
            {
                animalType = value;
            }
        }
        /// <summary>
        /// 动物编号
        /// </summary>
        public string animalId;
        private string AnimalId
        {
            get { return animalId; }
            set
            {
                animalId = value;
            }
        }
        /// <summary>
        /// 动物名称
        /// </summary>
        public string animalName;
        private string AnimalName
        {
            get { return animalName; }
            set
            {
                animalName = value;
            }
        }
        /// <summary>
        /// 动物描述
        /// </summary>
        public string animalDescription;
        private string AnimalDescription
        {
            get { return animalDescription; }
            set
            {
                animalDescription = value;
            }
        }

        /// <summary>
        /// 动物线索
        /// </summary>
        public List<string> animalClew;
        private List<string> AnimalClew
        {
            get { return animalClew; }
            set
            {
                animalClew = value;
            }
        }

        /// <summary>
        /// 文字提示
        /// </summary>
        public string animalTips;
        private string AnimalTips
        {
            get { return animalTips; }
            set
            {
                animalTips = value;
            }
        }


        /// <summary>
        /// 动物线索
        /// </summary>
        public List<string> animalDialogue;
        private List<string> AnimalDialogue
        {
            get { return animalDialogue; }
            set
            {
                animalDialogue = value;
            }
        }
    }

}
