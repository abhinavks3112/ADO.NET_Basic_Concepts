<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SqlBulkCopyWebForm.aspx.cs" Inherits="ADODOTNET_Concepts_WebApplication.SqlBulkCopyWebForm" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>

            <asp:Button ID="btnSqlBulkCopy" runat="server" OnClick="btnSqlBulkCopy_Click" Text="Bulk Copy to Sql" />

        </div>
    </form>
</body>
</html>
