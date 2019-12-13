/*
 * Created by SharpDevelop.
 * User: zack
 * Date: 2019/01/09
 * Time: 13:19
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Windows.Forms;
using System.Collections.Generic;
using System.ComponentModel;
using namaichi.info;

namespace namaichi
{
	/// <summary>
	/// Description of sortableList.
	/// </summary>
	public class SortableBindingList<T> : BindingList<T> where T : class
    {
        private bool _isSorted;
        private ListSortDirection _sortDirection = ListSortDirection.Ascending;
        private PropertyDescriptor _sortProperty;
        
        private string columnName = null;
        public config.config cfg = null; 
        public bool isFavoriteUp = false;
 
        /// <summary>
        /// Initializes a new instance of the <see cref="SortableBindingList{T}"/> class.
        /// </summary>
        public SortableBindingList()
        {
        }
 
        /// <summary>
        /// Initializes a new instance of the <see cref="SortableBindingList{T}"/> class.
        /// </summary>
        /// <param name="list">An <see cref="T:System.Collections.Generic.IList`1" /> of items to be contained in the <see cref="T:System.ComponentModel.BindingList`1" />.</param>
        public SortableBindingList(IList<T> list)
            :base(list)
        {
        }
 
        /// <summary>
        /// Gets a value indicating whether the list supports sorting.
        /// </summary>
        protected override bool SupportsSortingCore
        {
            get { return true; }
        }
 
        /// <summary>
        /// Gets a value indicating whether the list is sorted.
        /// </summary>
        protected override bool IsSortedCore
        {
            get { return _isSorted; }
        }
 
        /// <summary>
        /// Gets the direction the list is sorted.
        /// </summary>
        protected override ListSortDirection SortDirectionCore
        {
            get { return _sortDirection; }
        }
 
        /// <summary>
        /// Gets the property descriptor that is used for sorting the list if sorting is implemented in a derived class; otherwise, returns null
        /// </summary>
        protected override PropertyDescriptor SortPropertyCore
        {
            get { return _sortProperty; }
        }
 
        /// <summary>
        /// Removes any sort applied with ApplySortCore if sorting is implemented
        /// </summary>
        protected override void RemoveSortCore()
        {
            _sortDirection = ListSortDirection.Ascending;
            _sortProperty = null;
            _isSorted = false; //thanks Luca
        }
 
        /// <summary>
        /// Sorts the items if overridden in a derived class
        /// </summary>
        /// <param name="prop"></param>
        /// <param name="direction"></param>
        protected override void ApplySortCore(PropertyDescriptor prop, ListSortDirection direction)
        {
            _sortProperty = prop;
            _sortDirection = direction;
            
            columnName = prop.Name;
 
            List<T> list = Items as List<T>;
            if (list == null) return;
 
            
                
            list.Sort(Compare);
 
            _isSorted = true;
            //fire an event that the list has been changed.
            //OnListChanged(new ListChangedEventArgs(ListChangedType.Reset, -1));
        }
 
 
        private int Compare(T lhs, T rhs)
        {
            var result = OnComparison(lhs, rhs);
            //invert if descending
            if (_sortDirection == ListSortDirection.Descending)
                result = -result;
            return result;
        }
 
        private int OnComparison(T lhs, T rhs)
        {
            object lhsValue = lhs == null ? null : _sortProperty.GetValue(lhs);
            object rhsValue = rhs == null ? null : _sortProperty.GetValue(rhs);
            if (lhsValue == null)
            {
                return (rhsValue == null) ? 0 : -1; //nulls are equal
            }
            if (rhsValue == null)
            {
                return 1; //first has value, second doesn't
            }
            /*
            try {
	            if (isFavoriteUp && false) {
	            	var isFavL = ((LiveInfo)(object)lhs).favorite == "";
	            	var isFavR = ((LiveInfo)(object)rhs).favorite == "";
	            	if (isFavL && !isFavR) return 1;
	            	else if (!isFavL && isFavR) return -1;
	            }
            } catch (Exception e) {util.debugWriteLine(e.Message + e.Source + e.StackTrace + e.TargetSite);}
            */
            if (columnName == "HostId") {
            	int intL, intR;
            	var _l = int.TryParse(lhsValue.ToString(), out intL);
            	var _r = int.TryParse(rhsValue.ToString(), out intR);
            	if (_l && _r) return intL.CompareTo(intR);
            	else if (_l && !_r) return 1;
            	else if (!_l && _r) return -1;
            	return 0;
            }
            if (columnName == "elapsedTime") {
            	DateTime intL, intR;
            	var _l = DateTime.TryParse(lhsValue.ToString().Replace("間", ""), out intL);
            	var _r = DateTime.TryParse(rhsValue.ToString().Replace("間", ""), out intR);
            	if (_l && _r) return intL.CompareTo(intR);
            	else if (_l && !_r) return 1;
            	else if (!_l && _r) return -1;
            	return 0;
            }
            
            if (lhsValue is IComparable)
            {
                return ((IComparable)lhsValue).CompareTo(rhsValue);
            }
            if (lhsValue.Equals(rhsValue))
            {
                return 0; //both are the same
            }
            //not comparable, compare ToString
            return lhsValue.ToString().CompareTo(rhsValue.ToString());
        }
        
    }
}
