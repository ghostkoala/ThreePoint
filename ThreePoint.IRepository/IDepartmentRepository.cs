using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using ThreePoint.Core.Enities;
using ThreePoint.Core.Enities.ServiceModel;
using ThreePoint.Core.Filters;
using ThreePoint.Core.ServerModels;
using ThreePoint.Core.ViewModel;

namespace ThreePoint.IRepository
{
    public interface IDepartmentRepository
    {
        /// <summary>
        /// 下拉菜单搜索
        /// </summary>
        /// <param name="filter">过滤</param>
        /// <returns></returns>
        Task<List<DropDownDepartmentViewModel>> DropDownDepartmentSeachAsync(DropDownDepartmentFilter filter);

        /// <summary>
        /// 获取部门
        /// </summary>
        /// <param name="filter">过滤</param>
        /// <returns></returns>
        Task<PageResult<DepartmentEntity>> GetAsync(Expression<Func<DepartmentEntity, bool>> whereLambda, BaseFilter filter);

        /// <summary>
        /// 删除部门
        /// </summary>
        /// <param name="ids">ids</param>
        /// <returns></returns>
        Task<bool> DeleteAsync(IEnumerable<string> ids);

        /// <summary>
        /// 增加部门
        /// </summary>
        /// <param name="dto">数据</param>
        /// <returns></returns>
        Task<bool> AddAsync(DepartmentDto dto);

        /// <summary>
        /// 修改部门
        /// </summary>
        /// <param name="dto">数据</param>
        /// <returns></returns>
        Task<bool> EditAsync(DepartmentDto dto);

        /// <summary>
        /// 查找部门
        /// </summary>
        /// <param name="id">Id</param>
        /// <returns></returns>
        Task<DepartmentEntity> FindAsync(string id);

        /// <summary>
        /// 搜索部门id、名称
        /// </summary>
        /// <param name="keyWord">关键字</param>
        /// <returns></returns>
        Task<Dictionary<string, string>> GetSearchNameForAreaTextAsync(string keyWord);
    }
}