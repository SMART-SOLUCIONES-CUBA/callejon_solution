namespace SmartSolucionesCuba.CPCC.PCM.Terminal.NMMediaStore.Managers
{
    public class SubscriptionManager : Facade.ISubscriptionManagerFacade
    {
        private const string FIRST_NAME_LDAP_USER_ATTRIBUTE = "givenName";
        private const string LAST_NAME_LDAP_USER_ATTRIBUTE = "sn";
        private const string THUMBNAIL_PHOTO_LDAP_USER_ATTRIBUTE = "thumbnailPhoto";

        private const string AUTHORITY = "ElCallejon.net";

        private const string TARGET_API_BASE_QUERY = "mediastore/subscriptions";

        private Models.SubscriptorModel authenticatedSubscriptor;

        public async System.Threading.Tasks.Task AuthenticateAsync(string id, string password)
        {
            var userPrincipalName = string.Format("{1}@{0}", "ElCallejon.net", id);

            var ldapConnetion = new Novell.Directory.Ldap.LdapConnection();

            ldapConnetion.Connect(AUTHORITY, Novell.Directory.Ldap.LdapConnection.DefaultPort);

            ldapConnetion.Bind(Novell.Directory.Ldap.LdapConnection.LdapV3, userPrincipalName, password);

            var ldapConnetionSearchConstraints = ldapConnetion.SearchConstraints;

            ldapConnetionSearchConstraints.ReferralFollowing = true;

            ldapConnetion.Constraints = ldapConnetionSearchConstraints;

            var searchResultQueue = ldapConnetion.Search("DC=ElCallejon,DC=net", Novell.Directory.Ldap.LdapConnection.ScopeSub, string.Format("(&(objectClass=user)(objectClass=person)(sAMAccountName={0}))", id), new string[] { FIRST_NAME_LDAP_USER_ATTRIBUTE, LAST_NAME_LDAP_USER_ATTRIBUTE, THUMBNAIL_PHOTO_LDAP_USER_ATTRIBUTE }, false, null, null);

            Novell.Directory.Ldap.LdapMessage message;

            while ((message = searchResultQueue.GetResponse()) != null)
            {
                if (!(message is Novell.Directory.Ldap.LdapSearchResult)) continue;

                var entry = (message as Novell.Directory.Ldap.LdapSearchResult).Entry;

                var attributeSet = entry.GetAttributeSet();

                var subscriptor = new Models.SubscriptorModel
                {
                    Id = id,
                    Name = attributeSet[FIRST_NAME_LDAP_USER_ATTRIBUTE].StringValue,
                    LastName = attributeSet[LAST_NAME_LDAP_USER_ATTRIBUTE].StringValue,
                    ThumbnailContent = attributeSet[THUMBNAIL_PHOTO_LDAP_USER_ATTRIBUTE].ByteValue
                };

                try
                {
                    subscriptor.Subscription = await Helpers.HttpClientHelper.Instance.GetAsync<SAPRESSC.Business.Billboard.Cinema.Web.Api.Common.Models.SubscriptionModel>(string.Format("{0}/{1}", TARGET_API_BASE_QUERY, id));
                }
                catch (Exceptions.UnspectedHttpStatusException unspectedHttpStatusException)
                {
                    if (unspectedHttpStatusException.HttpStatusCode != System.Net.HttpStatusCode.NoContent) throw;
                }
                catch
                {
                    throw;
                }

                authenticatedSubscriptor = subscriptor;

                break;
            }

            ldapConnetion.Disconnect();
        }

        public Models.SubscriptorModel GetAuthenticatedSubscritor()
        {
            return authenticatedSubscriptor;
        }

        public async System.Threading.Tasks.Task PersistSubscriptionAsync(SAPRESSC.Business.Billboard.Cinema.Web.Api.Common.Models.SubscriptionModel model)
        {
            model.Id = authenticatedSubscriptor.Id;

            await Helpers.HttpClientHelper.Instance.PostAsync(TARGET_API_BASE_QUERY, model);

            authenticatedSubscriptor.Subscription = model;
        }
    }
}
