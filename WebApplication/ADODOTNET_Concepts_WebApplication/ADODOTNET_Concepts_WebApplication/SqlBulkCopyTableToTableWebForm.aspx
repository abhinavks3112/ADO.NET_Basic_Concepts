<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SqlBulkCopyTableToTableWebForm.aspx.cs" Inherits="ADODOTNET_Concepts_WebApplication.SqlBulkCopyTableToTableWebForm" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <asp:Button ID="btnSqlBulkCopy" runat="server" OnClick="btnSqlBulkCopy_Click" Text="Copy Data From One Table To Another" />
        </div>
    </form>
</body>
</html>
