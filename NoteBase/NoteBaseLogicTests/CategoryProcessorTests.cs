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
            Guid personId = Guid.Parse("4e8d41a5-790a-4a11-b6c2-b4d37b6fd38f");

            //act
            Category actual = categoryProcessor.Create(titel, personId);

            //assert
            Category expected = new(Guid.Parse("de55078e-2426-4a1f-b6bf-d3b288022eda"), "School", Guid.Parse("4e8d41a5-790a-4a11-b6c2-b4d37b6fd38f"));

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
            Guid personId = Guid.Parse("4e8d41a5-790a-4a11-b6c2-b4d37b6fd38f");

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
            Guid personId = Guid.Parse("4e8d41a5-790a-4a11-b6c2-b4d37b6fd38f");

            //act
            categoryProcessor.Create(titel, personId);
        }

        [TestMethod()]
        public void DeleteTest_Succeed()
        {
            //arrange
            ICategoryProcessor categoryProcessor = Factory.CreateCategoryProcessor();

            //act
            categoryProcessor.Delete(Guid.Parse("3fceb4e1-6fa5-41c0-9fbf-77cec3b7aec9"));

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
            categoryProcessor.Delete(Guid.Parse("b8e0725f-2672-4c19-87b7-c92d8f5c008d"));
        }

        [TestMethod()]
        [ExpectedException(typeof(Exception),
            "Category doesn't exist")]
        public void DeleteTest_Failed_CategoryDoesNotExist()
        {
            //arrange
            ICategoryProcessor categoryProcessor = Factory.CreateCategoryProcessor();

            //act
            categoryProcessor.Delete(Guid.Parse("12345678-1234-1234-1234-123456789123"));
        }

        [TestMethod()]
        public void UpdateTest_Succeed()
        {
            //arrange
            ICategoryProcessor categoryProcessor = Factory.CreateCategoryProcessor();
            Guid id = Guid.Parse("f2a2f10b-aafd-43c2-b848-12421f1fa88f");
            string titel = "Games";
            Guid personId = Guid.Parse("4e8d41a5-790a-4a11-b6c2-b4d37b6fd38f");

            //act
            Category actual = categoryProcessor.Update(id, titel, personId);

            //assert
            Category expected = new(Guid.Parse("f2a2f10b-aafd-43c2-b848-12421f1fa88f"), "Games", Guid.Parse("4e8d41a5-790a-4a11-b6c2-b4d37b6fd38f"));

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
            Guid id = Guid.Parse("00000000-0000-0000-0000-000000000000");
            string titel = "Games";
            Guid personId = Guid.Parse("4e8d41a5-790a-4a11-b6c2-b4d37b6fd38f");

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
            Guid id = Guid.Parse("f2a2f10b-aafd-43c2-b848-12421f1fa88f");
            string titel = "";
            Guid personId = Guid.Parse("4e8d41a5-790a-4a11-b6c2-b4d37b6fd38f");

            //act
            categoryProcessor.Update(id, titel, personId);
        }
    }
}