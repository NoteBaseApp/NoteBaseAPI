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
            string titel = "School";
            int personId = 1;

            //act
            Category actual = categoryProcessor.Create(titel, personId);

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
            string titel = "NOGames";
            int personId = 1;

            //act
            categoryProcessor.Create(titel, personId);
        }

        [TestMethod()]
        [ExpectedException(typeof(ArgumentException),
            "Title can't be empty")]
        public void CreateTest_Failed_TitleEmpty()
        {
            //arrange
            ICategoryProcessor categoryProcessor = Factory.CreateCategoryProcessor();
            string titel = "";
            int personId = 1;

            //act
            categoryProcessor.Create(titel, personId);
        }

        [TestMethod()]
        public void DeleteTest_Succeed()
        {
            //arrange
            ICategoryProcessor categoryProcessor = Factory.CreateCategoryProcessor();

            //act
            categoryProcessor.Delete(1);

            //assert
            //how do i test this?
            Assert.IsTrue(true);
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
            int id = 1;
            string titel = "Games";
            int personId = 1;

            //act
            Category actual = categoryProcessor.Update(id, titel, personId);

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
            int id = 999;
            string titel = "Games";
            int personId = 1;

            //act
            categoryProcessor.Update(id, titel, personId);
        }

        [TestMethod()]
        [ExpectedException(typeof(ArgumentException),
            "Title can't be empty")]
        public void UpdateTest_Failed_TitleEmpty()
        {
            //arrange
            ICategoryProcessor categoryProcessor = Factory.CreateCategoryProcessor();
            int id = 1;
            string titel = "";
            int personId = 1;

            //act
            categoryProcessor.Update(id, titel, personId);
        }
    }
}