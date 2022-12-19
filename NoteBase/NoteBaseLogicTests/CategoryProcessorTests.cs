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
            Category category = new(0, "School", 1);

            //act
            Response<Category> actual = categoryProcessor.Create(category);

            //assert
            Category expectedCategory = new(12, "School", 1);
            Response<Category> expected = new(true);
            expected.AddItem(expectedCategory);

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
            Category category = new(0, "Games", 1);

            //act
            Response<Category> actual = categoryProcessor.Create(category);

            //assert
            Response<Category> expected = new(false)
            {
                Message = "Category With this title already exists"
            };

            Assert.AreEqual(expected.Succeeded, actual.Succeeded);
            Assert.AreEqual(expected.Message, actual.Message);
        }

        [TestMethod()]
        public void CreateTest_Failed_TitleEmpty()
        {
            //arrange
            ICategoryProcessor categoryProcessor = Factory.CreateCategoryProcessor();
            Category category = new(0, "", 1);

            //act
            Response<Category> actual = categoryProcessor.Create(category);

            //assert
            Response<Category> expected = new(false)
            {
                Message = "Title can't be empty"
            };

            Assert.AreEqual(expected.Succeeded, actual.Succeeded);
            Assert.AreEqual(expected.Message, actual.Message);
        }

        [TestMethod()]
        public void DeleteTest_Succeed()
        {
            //arrange
            ICategoryProcessor categoryProcessor = Factory.CreateCategoryProcessor();

            //act
            Response<Category> actual = categoryProcessor.Delete(1);

            //assert
            Response<Category> expected = new(true);

            Assert.AreEqual(expected.Succeeded, actual.Succeeded);
        }

        [TestMethod()]
        public void DeleteTest_Failed_NoteConnection()
        {
            //arrange
            ICategoryProcessor categoryProcessor = Factory.CreateCategoryProcessor();

            //act
            Response<Category> actual = categoryProcessor.Delete(2);

            //assert
            Response<Category> expected = new(false)
            {
                Message = "Notes exist with this category"
            };

            Assert.AreEqual(expected.Succeeded, actual.Succeeded);
            Assert.AreEqual(expected.Message, actual.Message);
        }

        [TestMethod()]
        public void DeleteTest_Failed_CategoryDoesNotExist()
        {
            //arrange
            ICategoryProcessor categoryProcessor = Factory.CreateCategoryProcessor();

            //act
            Response<Category> actual = categoryProcessor.Delete(999);

            //assert
            Response<Category> expected = new(false)
            {
                Message = "Category doesn't exist"
            };

            Assert.AreEqual(expected.Succeeded, actual.Succeeded);
            Assert.AreEqual(expected.Message, actual.Message);
        }

        [TestMethod()]
        public void UpdateTest_Succeed()
        {
            //arrange
            ICategoryProcessor categoryProcessor = Factory.CreateCategoryProcessor();
            Category category = new(1, "Games", 1);

            //act
            Response<Category> actual = categoryProcessor.Update(category);

            //assert
            Category expectedCategory = new(1, "Games", 1);
            Response<Category> expected = new(true);
            expected.AddItem(expectedCategory);

            Assert.AreEqual(expected.Succeeded, actual.Succeeded);
            Assert.AreEqual(expected.Data[0].ID, actual.Data[0].ID);
            Assert.AreEqual(expected.Data[0].Title, actual.Data[0].Title);
            Assert.AreEqual(expected.Data[0].PersonId, actual.Data[0].PersonId);
        }

        [TestMethod()]
        public void UpdateTest_Failed_CategoryDoesNotExist()
        {
            //arrange
            ICategoryProcessor categoryProcessor = Factory.CreateCategoryProcessor();
            Category category = new(999, "Games", 1);

            //act
            Response<Category> actual = categoryProcessor.Update(category);

            //assert
            Response<Category> expected = new(false)
            {
                Message = "Category doesn't exist"
            };

            Assert.AreEqual(expected.Succeeded, actual.Succeeded);
            Assert.AreEqual(expected.Message, actual.Message);
        }

        [TestMethod()]
        public void UpdateTest_Failed_TitleEmpty()
        {
            //arrange
            ICategoryProcessor categoryProcessor = Factory.CreateCategoryProcessor();
            Category category = new(1, "", 1);

            //act
            Response<Category> actual = categoryProcessor.Update(category);

            //assert
            Response<Category> expected = new(false)
            {
                Message = "Title can't be empty"
            };

            Assert.AreEqual(expected.Succeeded, actual.Succeeded);
            Assert.AreEqual(expected.Message, actual.Message);
        }
    }
}