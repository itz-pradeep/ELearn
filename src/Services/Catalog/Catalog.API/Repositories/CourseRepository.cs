﻿using Catalog.API.Data;
using Catalog.API.Entities;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Catalog.API.Repositories
{
    public class CourseRepository : ICourseRepository
    {
        private readonly ICatalogContext _context;

        public CourseRepository(ICatalogContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }
        public async Task CreateCourseAsync(Course entity)
        {
            await _context.Courses.InsertOneAsync(entity);
        }

        public async Task<bool> DeleteCourseAsync(string id)
        {
            FilterDefinition<Course> filter = Builders<Course>.Filter.Eq(c => c.Id, id);

            var deleteResult = await _context.Courses.DeleteOneAsync(filter);

            return deleteResult.IsAcknowledged
               && deleteResult.DeletedCount > 0;    
        }

        public async Task<Course> GetCourseAsync(string id)
        {
            return await _context.Courses.Find(c => c.Id == id).FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<Course>> GetCourseByTechnologyAsync(string Technology)
        {
            FilterDefinition<Course> filter = Builders<Course>.Filter.ElemMatch(c =>c.Technology, Technology);
            
            return await _context.Courses.Find(filter).ToListAsync();
        }

        public async Task<IEnumerable<Course>> GetCoursesAsync()
        {
            return await _context.Courses.Find(c => true).ToListAsync();
        }

        public async Task<bool> UpdateCourseAsync(Course entity)
        {
            var updateResult = await _context.Courses.ReplaceOneAsync(filter: c => c.Id == entity.Id, replacement: entity);

            return updateResult.IsAcknowledged 
                && updateResult.ModifiedCount > 0;
        }
    }
}