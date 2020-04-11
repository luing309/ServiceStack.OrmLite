using Npgsql;

namespace ServiceStack.OrmLite.PostgreSQL
{
    public static class PgSql
    {
        public static NpgsqlParameter Param<T>(string name, T value) =>
            new NpgsqlParameter(name, PostgreSqlDialect.Instance.GetDbType<T>()) {
                Value = value
            };

        public static string Array<T>(params T[] items) =>
            "ARRAY[" + PostgreSqlDialect.Provider.SqlSpread(items) + "]";

        public static string Array<T>(T[] items, bool nullIfEmpty) => nullIfEmpty
            ? "ARRAY[" + NullIfEmpty(PostgreSqlDialect.Provider.SqlSpread(items)) + "]"
            : "ARRAY[" + PostgreSqlDialect.Provider.SqlSpread(items) + "]";

        public static string NullIfEmpty(string sql) => sql == "ARRAY[]" ? "null" : sql;
    }
}