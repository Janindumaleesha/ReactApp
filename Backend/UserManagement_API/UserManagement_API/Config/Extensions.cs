using System.Security.Claims;
using UserManagement_API.Models;
using UserManagement_API.Models.UserModels;

namespace UserManagement_API.Config
{
    public static class Extensions
    {
        internal static ModelUserJWTInfo AuthenticateInfo(this ClaimsPrincipal User)
        {
            var claimIdentity = User.Identity as ClaimsIdentity;

            if (claimIdentity is null) { return new ModelUserJWTInfo(); }

            return new ModelUserJWTInfo()
            {
                UserName = claimIdentity.FindFirst(ClaimTypes.Name)?.Value,
                SystemRole = claimIdentity.FindFirst(ClaimTypes.Role)?.Value
            };
        }

        internal static bool IsNullOrNonPositive<T>(this T? t, int? index = null) where T : struct, IComparable<T>
        {
            if (index is not null)
            {
                return t is null || t.Value.CompareTo(default) < index;
            }

            return t is null || t.Value.CompareTo(default) <= 0;
        }

        internal static bool IsNullOrNonPositive<T>(this T t, int? index = null) where T : struct, IComparable<T>
        {
            if (index is not null)
            {
                return t.CompareTo(default) < index;
            }

            return t.CompareTo(default) <= 0;
        }

        internal static bool IsInvalid<T>(this IEnumerable<T> list)
        {
            return list is null || !list.Any();
        }

        internal static PaginationInfo CalculatePaginationInfo(this int _rowCount, int _page, int _rows)
        {
            var _totalPages = (int)Math.Ceiling((double)_rowCount / _rows);

            return new PaginationInfo()
            {
                TotalPages = _totalPages,
                Page = _page,
                NextPage = (_page + 1) <= _totalPages ? _page + 1 : -1,
                RowCount = _rowCount
            };
        }
    }
}
