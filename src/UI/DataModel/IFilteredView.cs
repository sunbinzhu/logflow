﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataModel
{
    public interface IFilteredView<T> where T : DataItemBase
    {
        #region Find, Count, Filter, Tag features.

        /// <summary>
        /// Tag or replace the tag's filter.
        /// </summary>
        /// <param name="index">the tag index</param>
        /// <param name="filter">the filter</param>
        void Tag(int index, Filter filter);
        void UnTag(int index);

        /// <summary>
        /// Find the next occurrence. Yielding progress from 0 to 100, if -1 is yielded, it means no result till the end of the current direction.
        /// Continue iterating the progress will search from the other end. If -2 is yielded, it means no result for all items.
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="currentIndex"></param>
        /// <param name="direction"></param>
        /// <param name="result"></param>
        /// <returns></returns>
        IEnumerable<int> Find(Filter filter, int currentIndex, bool direction, ResultWrapper<int> result);

        IEnumerable<int> Count(Filter filter, ResultWrapper<int> result);

        IEnumerable<int> Initialize();

        bool IsInitialized { get; }

        #endregion

        #region Events

        event EventHandler<int> ItemAdded;

        #endregion

        #region Child

        FilteredView<T> CreateChild(Filter filter);

        #endregion

        #region Display

        int? FirstDisplayedScrollingRowIndex { get; set; }

        string Name { get; }

        int TotalCount { get; }

        object GetColumnValue(int rowIndex, int columnIndex);

        int GetPhysicalIndex(int logicalIndex);

        IList<ColumnInfoAttribute> ColumnInfos { get; }

        #endregion
    }
}