using System;
using System.Collections.Generic;
using System.Text;

namespace Library.Models
{
	public class Permission
	{
		public int Id { get; set; }
		public int PermissionItemId { get; set; }
		public PermissionItem? PermissionItem { get; set; }
		public int PermissionActionId { get; set; }
		public PermissionAction? PermissionAction { get; set; }
		public Guid UserId { get; set; }
		public Guid StoreId { get; set; }

	}

	public class PermissionItem
	{
		public int Id { get; set; }
		public string ItemName { get; set; } = string.Empty;
	}
	public class PermissionAction
	{
		public int Id { get; set; }
		public string ActionName { get; set; } = string.Empty;
	}
}
