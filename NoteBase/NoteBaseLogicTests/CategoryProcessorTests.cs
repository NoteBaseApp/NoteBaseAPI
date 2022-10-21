using Microsoft.VisualStudio.TestTools.UnitTesting;
using NoteBaseDALInterface;
using NoteBaseLogic;
using NoteBaseLogicInterface;
using NoteBaseLogicInterface.Models;
using NoteBaseLogicTests.TestDALs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NoteBaseLogic.Tests
{
    [TestClass()]
    public class CategoryProcessorTests
    {
        [TestMethod()]
        public void CreateTest_Succeed()
        {
            //arrange
            ICategoryDAL categoryDAL = new CreateTest_SucceedDAL();
            ICategoryProcessor categoryProcessor = NoteBaseLogicFactory.ProcessorFactory.CreateCategoryProcessor(categoryDAL);
            Category category = new(0, "Test", 1);

            Response<Category> expected = new(true);

            //act
            Response<Category> actual = categoryProcessor.Create(category);

            //assert
            Assert.AreEqual(expected.Succeeded, actual.Succeeded);
            Assert.AreEqual(expected.Code, actual.Code);
        }

        [TestMethod()]
        public void CreateTest_Failed_TitleAreadyExists()
        {
            //arrange
            ICategoryDAL categoryDAL = new CreateTest_Faled_TitleAreadyExists();
            ICategoryProcessor categoryProcessor = NoteBaseLogicFactory.ProcessorFactory.CreateCategoryProcessor(categoryDAL);
            Category category = new(0, "Test", 1);

            Response<Category> expected = new(false)
            {
                Code = 2627
            };

            //act
            Response<Category> actual = categoryProcessor.Create(category);

            //assert
            Assert.AreEqual(expected.Succeeded, actual.Succeeded);
            Assert.AreEqual(expected.Code, actual.Code);
        }

        [TestMethod()]
        public void CreateTest_Failed_NoTitle()
        {
            //arrange
            ICategoryDAL categoryDAL = new CreateTest_SucceedDAL();
            ICategoryProcessor categoryProcessor = NoteBaseLogicFactory.ProcessorFactory.CreateCategoryProcessor(categoryDAL);
            Category category = new(0, "", 1);

            Response<Category> expected = new(false)
            {
                Message = "Title cant be empty"
            };

            //act
            Response<Category> actual = categoryProcessor.Create(category);

            //assert
            Assert.AreEqual(expected.Succeeded, actual.Succeeded);
            Assert.AreEqual(expected.Message, actual.Message);
        }
    }
}