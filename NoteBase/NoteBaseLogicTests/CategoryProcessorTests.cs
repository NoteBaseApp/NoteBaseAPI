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
            Category actual = categoryProcessor.Create(category);

            //assert
            Category expected = new(12, "School", 1);

            Assert.AreEqual(expected.ID, actual.ID);
            Assert.AreEqual(expected.Title, actual.Title);
            Assert.AreEqual(expected.PersonId, actual.PersonId);
        }

        [TestMethod()]
        [ExpectedException(typeof(Exception),
            "Category With this title already exists")]
        public void CreateTest_Failed_TitleAreadyExists()
        {
            //arrange
            ICategoryProcessor categoryProcessor = Factory.CreateCategoryProcessor();
            Category category = new(0, "Games", 1);

            //act
            categoryProcessor.Create(category);
        }

        [TestMethod()]
        [ExpectedException(typeof(ArgumentException),
            "Title can't be empty")]
        public void CreateTest_Failed_TitleEmpty()
        {
            //arrange
            ICategoryProcessor categoryProcessor = Factory.CreateCategoryProcessor();
            Category category = new(0, "", 1);

            //act
            categoryProcessor.Create(category);
        }

        [TestMethod()]
        public void DeleteTest_Succeed()
        {
            //arrange
            ICategoryProcessor categoryProcessor = Factory.CreateCategoryProcessor();

            //act
            int actual = categoryProcessor.Delete(1);

            //assert
            int expected = 1;

            Assert.AreEqual(expected, actual);
        }

        [TestMethod()]
        [ExpectedException(typeof(Exception),
            "Notes exist with this category")]
        public void DeleteTest_Failed_NoteConnection()
        {
            //arrange
            ICategoryProcessor categoryProcessor = Factory.CreateCategoryProcessor();

            //act
            categoryProcessor.Delete(2);
        }

        [TestMethod()]
        [ExpectedException(typeof(Exception),
            "Category doesn't exist")]
        public void DeleteTest_Failed_CategoryDoesNotExist()
        {
            //arrange
            ICategoryProcessor categoryProcessor = Factory.CreateCategoryProcessor();

            //act
            categoryProcessor.Delete(999);
        }

        [TestMethod()]
        public void UpdateTest_Succeed()
        {
            //arrange
            ICategoryProcessor categoryProcessor = Factory.CreateCategoryProcessor();
            Category category = new(1, "Games", 1);

            //act
            Category actual = categoryProcessor.Update(category);

            //assert
            Category expected = new(1, "Games", 1);

            Assert.AreEqual(expected.ID, actual.ID);
            Assert.AreEqual(expected.Title, actual.Title);
            Assert.AreEqual(expected.PersonId, actual.PersonId);
        }

        [TestMethod()]
        [ExpectedException(typeof(Exception),
            "Category doesn't exist")]
        public void UpdateTest_Failed_CategoryDoesNotExist()
        {
            //arrange
            ICategoryProcessor categoryProcessor = Factory.CreateCategoryProcessor();
            Category category = new(999, "Games", 1);

            //act
            categoryProcessor.Update(category);
        }

        [TestMethod()]
        [ExpectedException(typeof(ArgumentException),
            "Title can't be empty")]
        public void UpdateTest_Failed_TitleEmpty()
        {
            //arrange
            ICategoryProcessor categoryProcessor = Factory.CreateCategoryProcessor();
            Category category = new(1, "", 1);

            //act
            categoryProcessor.Update(category);
        }
    }
}