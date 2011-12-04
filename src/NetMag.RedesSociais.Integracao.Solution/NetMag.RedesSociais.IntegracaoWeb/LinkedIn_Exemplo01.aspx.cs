using System;
using LinkedIn;

namespace NetMag.RedesSociais.IntegracaoWeb
{
    public partial class LinkedIn_Exemplo01 : LinkedInBasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            var service = new LinkedInService(base.Authorization);

            connectionsDataList.DataSource = 
                service.GetConnectionsForCurrentUser().Items;
            connectionsDataList.DataBind();
        }
    }
}