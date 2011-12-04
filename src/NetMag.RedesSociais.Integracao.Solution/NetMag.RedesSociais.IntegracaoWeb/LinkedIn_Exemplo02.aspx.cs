using System;
using System.Collections.Generic;

using LinkedIn;
using LinkedIn.ServiceEntities;

namespace NetMag.RedesSociais.IntegracaoWeb
{
    public partial class LinkedIn_Exemplo02 : LinkedInBasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            var service = new LinkedInService(base.Authorization);
            var fields = new List<string>
                             {
                                 "id",
                                 "first-name",
                                 "last-name",
                                 "headline",
                                 "current-status",
                                 "positions",
                                 "picture-url"
                             };

            DisplayProfile(service.GetCurrentUser(ProfileType.Standard, fields));
        }

        private void DisplayProfile(Person person)
        {
            if (person != null)
            {
                nameLiteral.Text = person.Name;
                headlineLiteral.Text = person.Headline;
                statusLiteral.Text = person.CurrentStatus;
                profileImage.ImageUrl = person.PictureUrl;
                profileImage.AlternateText = person.Name;

                positionsDataList.DataSource = person.Positions;
                positionsDataList.DataBind();
            }
        }
    }
}