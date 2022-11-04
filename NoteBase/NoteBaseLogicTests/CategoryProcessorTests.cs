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
            ICategoryProcessor categoryProcessor = Factory.CreateCategoryProcessor();
            Category category = new(0, "Test", 1);
            Category expectedCategory = new(12, "Test", 1);

            Response<Category> expected = new(true);
            expected.AddItem(expectedCategory);

            //act
            Response<Category> actual = categoryProcessor.Create(category);

            //assert
            Assert.AreEqual(expected.Succeeded, actual.Succeeded);

            Assert.AreEqual(expected.Data[0].ID, actual.Data[0].ID);
            Assert.AreEqual(expected.Data[0].Title, actual.Data[0].Title);
            Assert.AreEqual(expected.Data[0].PersonId, actual.Data[0].PersonId);
        }

        [TestMethod()]
        public void CreateTest_Failed_TitleAreadyExists()
        {
            //arrange
            ICategoryProcessor categoryProcessor = Factory.CreateCategoryProcessor();
            Category category = new(0, "TestExist", 1);

            Response<Category> expected = new(false)
            {
                Message = "Category With this title already exists"
            };

            //act
            Response<Category> actual = categoryProcessor.Create(category);

            //assert
            Assert.AreEqual(expected.Succeeded, actual.Succeeded);
            Assert.AreEqual(expected.Message, actual.Message);
        }

        [TestMethod()]
        public void CreateTest_Failed_TitleEmpty()
        {
            //arrange
            ICategoryProcessor categoryProcessor = Factory.CreateCategoryProcessor();
            Category category = new(0, "", 1);

            Response<Category> expected = new(false)
            {
                Message = "Title can't be empty"
            };

            //act
            Response<Category> actual = categoryProcessor.Create(category);

            //assert
            Assert.AreEqual(expected.Succeeded, actual.Succeeded);
            Assert.AreEqual(expected.Message, actual.Message);
        }

        [TestMethod()]
        public void DeleteTest_Succeed()
        {
            //arrange
            ICategoryProcessor categoryProcessor = Factory.CreateCategoryProcessor();

            Response<Category> expected = new(true);

            //act
            Response<Category> actual = categoryProcessor.Delete(1);

            //assert
            Assert.AreEqual(expected.Succeeded, actual.Succeeded);
        }

        [TestMethod()]
        public void DeleteTest_Failed_NoteConnection()
        {
            //arrange
            ICategoryProcessor categoryProcessor = Factory.CreateCategoryProcessor();

            Response<Category> expected = new(false) 
            { 
                Message = "Notes exist with this category"
            };

            //act
            Response<Category> actual = categoryProcessor.Delete(2);

            //assert
            Assert.AreEqual(expected.Succeeded, actual.Succeeded);
            Assert.AreEqual(expected.Message, actual.Message);
        }

        [TestMethod()]
        public void DeleteTest_Failed_CategoryDoesNotExist()
        {
            //arrange
            ICategoryProcessor categoryProcessor = Factory.CreateCategoryProcessor();

            Response<Category> expected = new(false)
            {
                Message = "Category doesn't exist"
            };

            //act
            Response<Category> actual = categoryProcessor.Delete(999);

            //assert
            Assert.AreEqual(expected.Succeeded, actual.Succeeded);
            Assert.AreEqual(expected.Message, actual.Message);
        }
    }
}