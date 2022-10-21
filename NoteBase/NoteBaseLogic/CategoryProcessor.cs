﻿using NoteBaseDAL;
using NoteBaseDALInterface;
using NoteBaseDALInterface.Models;
using NoteBaseLogicInterface;
using NoteBaseLogicInterface.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace NoteBaseLogic
{
    public class CategoryProcessor : ICategoryProcessor
    {
        private readonly ICategoryDAL CategoryDAL;
        private readonly INoteProcessor NoteProcessor;
        public CategoryProcessor(ICategoryDAL _categoryDAL, INoteProcessor _noteProcessor)
        {
            CategoryDAL = _categoryDAL;
            NoteProcessor = _noteProcessor;
        }

        public Response<Category> Create(Category _cat)
        {
            Response<Category> response = new(false);

            if (_cat.Title == "")
            {
                response.Message = "Title cant be empty";

                return response;
            }

            DALResponse<CategoryDTO> catDALreponse = CategoryDAL.Create(_cat.ToDTO());

            response = new(catDALreponse.Succeeded)
            {
                Message = catDALreponse.Message,
                Code = catDALreponse.Code
            };

            return response;
        }

        public Response<Category> Delete(int _catId)
        {
            DALResponse<CategoryDTO> catDALreponse = CategoryDAL.Delete(_catId);

            Response<Category> response = new(catDALreponse.Succeeded)
            {
                Message = catDALreponse.Message,
                Code = catDALreponse.Code
            };

            return response;
        }

        public Response<Category> GetById(int _catId)
        {
            DALResponse<CategoryDTO> catDALreponse = CategoryDAL.GetById(_catId);

            Response<Category> categoryResponse = new(catDALreponse.Succeeded)
            {
                Message = catDALreponse.Message,
                Code = catDALreponse.Code
            };

            Category category = new(catDALreponse.Data[0].ID, catDALreponse.Data[0].Title, catDALreponse.Data[0].PersonId);

            Response<Note> noteProcessorReponse = NoteProcessor.GetByCategory(_catId);

            foreach (Note note in noteProcessorReponse.Data)
            {
                if (category.IsNoteCompatible(note))
                {
                    category.TryAddNote(note);
                }
            }

            categoryResponse.AddItem(category);

            return categoryResponse;
        }

        public Response<Category> GetByPerson(int _personId)
        {
            DALResponse<CategoryDTO> catDALreponse = CategoryDAL.GetByPerson(_personId);

            Response<Category> categoryResponse = new(catDALreponse.Succeeded)
            {
                Message = catDALreponse.Message,
                Code = catDALreponse.Code
            };

            foreach (CategoryDTO categoryDTO in catDALreponse.Data)
            {
                Category category = new(categoryDTO.ID, categoryDTO.Title, categoryDTO.PersonId);

                Response<Note> noteProcessorReponse = NoteProcessor.GetByCategory(category.ID);

                foreach (Note note in noteProcessorReponse.Data)
                {
                    if (category.IsNoteCompatible(note))
                    {
                        category.TryAddNote(note);
                    }
                }

                categoryResponse.AddItem(category);
            }

            return categoryResponse;
        }

        /* public Response<Category> GetByTitle(string _title)
        {
            DALResponse<CategoryDTO> catDALreponse = CategoryDAL.GetById(_catId);

            Response<Category> response = new(catDALreponse.Status, catDALreponse.Message);

            response.AddItem(new(catDALreponse.Data[0].ID, catDALreponse.Data[0].Title, catDALreponse.Data[0].PersonId));

            return response;
        } */

        public Response<Category> Update(Category _cat)
        {
            DALResponse<CategoryDTO> catDALreponse = CategoryDAL.Update(_cat.ToDTO());

            Response<Category> response = new(catDALreponse.Succeeded)
            {
                Message = catDALreponse.Message,
                Code = catDALreponse.Code
            };

            return response;
        }
    }
}
