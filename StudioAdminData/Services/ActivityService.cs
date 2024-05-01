﻿using Microsoft.EntityFrameworkCore;
using StudioData.Data;
using StudioData.Interfaces;
using StudioData.Models.Business;

namespace StudioData.Services
{
    public class ActivityService : IActivityService
    {
        private readonly StudioWebContext _context;
        private readonly ICommonServices<Activity> _commonContext;
        public ActivityService(StudioWebContext context, ICommonServices<Activity> commonContext)
        {
            _context = context;
            _commonContext = commonContext;            
        }

        public async Task<IEnumerable<Activity>> GetAllAsync()
        {
            return await _commonContext.GetAllAsync(); ;
        }
        public async Task<Activity> GetByIdAsync(Guid id)
        {
            return await _commonContext.GetByIdAsync(id);
        }
        public async Task<IEnumerable<Activity>> GetByRollAsync(Roles Rol)
        {
            var ActividyAndValue = await _context.Activities.Where(a => a.Roles == Rol).ToListAsync();
            return ActividyAndValue;
        }
        public async Task<bool> UpdateAsync(Activity activity)
        {
            return await _commonContext.UpdateAsync(activity);
        }
        public async Task<bool> InsertAsync(Activity activityValue)
        {
            return await _commonContext.InsertAsync(activityValue);
        }
        public async Task<bool> DeleteAsync(Guid Id)
        {
            return await _commonContext.DeleteAsync(Id);

        }
    }

}
