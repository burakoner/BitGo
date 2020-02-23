using BitGo.Objects;
using BitGo.Objects.Address;
using BitGo.Objects.Enterprise;
using BitGo.Objects.Key;
using BitGo.Objects.PendingApproval;
using BitGo.Objects.Shared;
using BitGo.Objects.Transfer;
using BitGo.Objects.User;
using BitGo.Objects.Wallet;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace BitGo
{
    public class BitGoClient
    {

        #region Properties
        public string EndpointUrl { get; private set; }
        public BitGoNetwork Network { get; private set; }
        public SecureString AccessToken { get; private set; }
        #endregion

        #region CTOR
        public BitGoClient(string accessToken, BitGoNetwork network = BitGoNetwork.Main)
        {
            this.Network = network;
            this.EndpointUrl = this.Network == BitGoNetwork.Main ? "https://www.bitgo.com/api/v2" : "https://test.bitgo.com/api/v2";
            this.SetAccessToken(accessToken);
        }
        #endregion

        #region Access Token
        public void SetAccessToken(string accessToken)
        {
            if (!string.IsNullOrEmpty(accessToken))
            {
                this.AccessToken = accessToken.StringToSecureString();
            }
        }
        #endregion

        #region Address
        /// <summary>
        /// List receive addresses on a wallet
        /// </summary>
        /// <param name="coin">Required. Example: "btc"</param>
        /// <param name="walletId">string /^[0-9a-f]{32}$/ Example: "585951a5df8380e0e3063e9f12345678</param>
        /// <param name="labelContains">A case-insensitive regular expression which will be used to filter returned addresses based on their address label.</param>
        /// <param name="limit">integer[1.. 500]. Default: 25. Maximum number of results to return. If the result set is truncated, use the nextBatchPrevId value to get the next batch.</param>
        /// <param name="mine">Default: false. Whether to return only the addresses which the current user has created.</param>
        /// <param name="prevId">Example: "585951a5df8380e0e3063e9f". Return the next batch of results, based on the nextBatchPrevId value from the previous batch.</param>
        /// <param name="chains">Items Enum:0 1 10 11 20 21. Replaces segwit. Mutually exclusive with segwit. Returns only unspents/addresses of the chains passed. If neither chains nor segwit is passed unspents/addresses from all chains are returned.</param>
        /// <param name="sort">Default: 1. Enum:-1 1. Sort order of returned addresses. (1 for ascending, -1 for descending).</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public WebCallResult<AddressInfoList> GetAddresses(
            string coin,
            string walletId,
            string labelContains = null,
            int limit = 25,
            bool mine = false,
            string prevId = null,
            string[] chains = null,
            int sort = 1,
            CancellationToken cancellationToken = default(CancellationToken))
            => this.GetAddressesAsync(coin, walletId, labelContains, limit, mine, prevId, chains, sort, cancellationToken).Result;
        public async Task<WebCallResult<AddressInfoList>> GetAddressesAsync(
            string coin,
            string walletId,
            string labelContains = null,
            int limit = 25,
            bool mine = false,
            string prevId = null,
            string[] chains = null,
            int sort = 1,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            if (limit < 1 || limit > 500)
                return WebCallResult<AddressInfoList>.CreateErrorResult(new ArgumentError("Limit should be between 1-500"));

            if (sort != 1 && sort != -1)
                return WebCallResult<AddressInfoList>.CreateErrorResult(new ArgumentError("Sort should be either 1 or -1"));

            var query = this.ConvertToQueryString(new Dictionary<string, object>() {
                { "labelContains", labelContains },
                { "limit", limit },
                { "mine", mine },
                { "prevId", prevId },
                { "chains", chains },
                { "sort", sort }
            });

            return await this.GetAsync<AddressInfoList>($"{this.EndpointUrl}/{coin}/wallet/{walletId}/addresses" + query, cancellationToken);
        }

        /// <summary>
        /// This API call is used to create a new receive address for your wallet. You may choose to call this API whenever a deposit is made. The BitGo API supports millions of addresses.
        /// Please check the "Coin-Specific Implementation" with regards to fee address management for Ethereum.
        /// </summary>
        /// <param name="coin">Example: "btc"</param>
        /// <param name="walletId">string /^[0-9a-f]{32}$/ Example: "585951a5df8380e0e3063e9f12345678"</param>
        /// <param name="chain">Enum:0 1 10 11 20 21</param>
        /// <param name="label">string Max 250 characters. A human-readable label which should be applied to the new address</param>
        /// <param name="gasPrice">Explicit gas price to use when deploying the forwarder contract (ETH only). If not given, defaults to the current estimated network gas price.</param>
        /// <param name="lowPriority">Default: false. Whether the deployment of the address forwarder contract should use a low priority fee key (ETH only)</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public WebCallResult<AddressInfo> CreateAddress(
            string coin,
            string walletId,
            int chain,
            string label,
            string gasPrice,
            bool lowPriority,
            CancellationToken cancellationToken = default(CancellationToken))
            => this.CreateAddressAsync(coin, walletId, chain, label, gasPrice, lowPriority, cancellationToken).Result;
        public async Task<WebCallResult<AddressInfo>> CreateAddressAsync(
            string coin,
            string walletId,
            int chain,
            string label,
            string gasPrice,
            bool lowPriority,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            var data = new RequestData_AddressCreate
            {
                Chain = chain,
                Label = label,
                GasPrice = gasPrice,
                LowPriority = lowPriority
            };

            return await this.PostAsync<AddressInfo>($"{this.EndpointUrl}/{coin}/wallet/{walletId}/address", data, cancellationToken);
        }

        /// <summary>
        /// Gets a receive address on a wallet
        /// </summary>
        /// <param name="coin">Example: "btc"</param>
        /// <param name="walletId">string /^[0-9a-f]{32}$/ Example: "585951a5df8380e0e3063e9f12345678"</param>
        /// <param name="addressOrId">Address or Address Id which will be used for information lookup</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public WebCallResult<AddressInfoWithBalance> GetAddress(
            string coin,
            string walletId,
            string addressOrId,
            CancellationToken cancellationToken = default(CancellationToken))
            => this.GetAddressAsync(coin, walletId, addressOrId, cancellationToken).Result;
        public async Task<WebCallResult<AddressInfoWithBalance>> GetAddressAsync(
            string coin,
            string walletId,
            string addressOrId,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            return await this.GetAsync<AddressInfoWithBalance>($"{this.EndpointUrl}/{coin}/wallet/{walletId}/address/{addressOrId}", cancellationToken);
        }

        /// <summary>
        /// Update a receive address on a wallet
        /// </summary>
        /// <param name="coin">Example: "btc"</param>
        /// <param name="walletId">string /^[0-9a-f]{32}$/ Example: "585951a5df8380e0e3063e9f12345678"</param>
        /// <param name="addressOrId">Address or Address Id which will be used for information lookup</param>
        /// <param name="label">string Max 250 characters. A human-readable label which should be applied to the new address</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public WebCallResult<AddressInfoWithBalance> UpdateAddress(
            string coin,
            string walletId,
            string addressOrId,
            string label,
            CancellationToken cancellationToken = default(CancellationToken))
            => this.UpdateAddressAsync(coin, walletId, addressOrId, label, cancellationToken).Result;
        public async Task<WebCallResult<AddressInfoWithBalance>> UpdateAddressAsync(
            string coin,
            string walletId,
            string addressOrId,
            string label,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            var data = new RequestData_AddressUpdate
            {
                Label = label,
            };

            return await this.PutAsync<AddressInfoWithBalance>($"{this.EndpointUrl}/{coin}/wallet/{walletId}/address/{addressOrId}", data, cancellationToken);
        }
        #endregion

        #region Enterprise
        /// <summary>
        /// Gets enterprises list
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public WebCallResult<EnterpriseInfoList> GetEnterprises(CancellationToken cancellationToken = default(CancellationToken)) => this.GetEnterprisesAsync(cancellationToken).Result;
        public async Task<WebCallResult<EnterpriseInfoList>> GetEnterprisesAsync(CancellationToken cancellationToken = default(CancellationToken))
        {
            return await this.GetAsync<EnterpriseInfoList>($"{this.EndpointUrl}/enterprise", cancellationToken);
        }

        /// <summary>
        /// CreateS an enterprise
        /// </summary>
        /// <param name="name">Required. Name of Organization</param>
        /// <param name="enterpriseUrl">Required. Url of Organization</param>
        /// <param name="emergencyPhone">Phone number for emergencies</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public WebCallResult<EnterpriseInfoObjectTags> CreateEnterprise(
            string name,
            string enterpriseUrl,
            string emergencyPhone = "",
            CancellationToken cancellationToken = default(CancellationToken))
            => this.CreateEnterpriseAsync(name, enterpriseUrl, emergencyPhone, cancellationToken).Result;
        public async Task<WebCallResult<EnterpriseInfoObjectTags>> CreateEnterpriseAsync(
            string name,
            string enterpriseUrl,
            string emergencyPhone = "",
            CancellationToken cancellationToken = default(CancellationToken))
        {
            var data = new RequestData_EnterpriseCreate
            {
                Name = name,
                EmergencyPhone = emergencyPhone,
                Url = enterpriseUrl
            };

            return await this.PostAsync<EnterpriseInfoObjectTags>($"{this.EndpointUrl}/enterprise", data, cancellationToken);
        }

        /// <summary>
        /// Gets an enterprise details
        /// </summary>
        /// <param name="enterpriseId">string /^[0-9a-f]{32}$/ Example: "585951a5df8380e0e3063e9f12345678"</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public WebCallResult<EnterpriseInfoObjectTags> GetEnterprise(
            string enterpriseId,
            CancellationToken cancellationToken = default(CancellationToken))
            => this.GetEnterpriseAsync(enterpriseId, cancellationToken).Result;
        public async Task<WebCallResult<EnterpriseInfoObjectTags>> GetEnterpriseAsync(
            string enterpriseId,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            return await this.GetAsync<EnterpriseInfoObjectTags>($"{this.EndpointUrl}/enterprise/{enterpriseId}", cancellationToken);
        }

        /// <summary>
        /// Updates number of required approvals  for an enterprise
        /// </summary>
        /// <param name="enterpriseId">string /^[0-9a-f]{32}$/ Example: "585951a5df8380e0e3063e9f12345678"</param>
        /// <param name="approvalsRequired"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public WebCallResult<PendingApprovalInfo> UpdateEnterprise(
            string enterpriseId,
            int approvalsRequired,
            CancellationToken cancellationToken = default(CancellationToken))
            => this.UpdateEnterpriseAsync(enterpriseId, approvalsRequired, cancellationToken).Result;
        public async Task<WebCallResult<PendingApprovalInfo>> UpdateEnterpriseAsync(
            string enterpriseId,
            int approvalsRequired,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            var data = new RequestData_EnterpriseUpdate
            {
                ApprovalsRequired = approvalsRequired,
            };

            return await this.PutAsync<PendingApprovalInfo>($"{this.EndpointUrl}/enterprise/{enterpriseId}", data, cancellationToken);
        }


        /// <summary>
        /// Lists enterprise users
        /// </summary>
        /// <param name="enterpriseId">string /^[0-9a-f]{32}$/ Example: "585951a5df8380e0e3063e9f12345678"</param>
        /// <param name="allowInactiveAdmins">Whether inactive admins should be returned as well</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public WebCallResult<EnterpriseUsers> GetEnterpriseUsers(
            string enterpriseId,
            bool allowInactiveAdmins,
            CancellationToken cancellationToken = default(CancellationToken))
            => this.GetEnterpriseUsersAsync(enterpriseId, allowInactiveAdmins, cancellationToken).Result;
        public async Task<WebCallResult<EnterpriseUsers>> GetEnterpriseUsersAsync(
            string enterpriseId,
            bool allowInactiveAdmins,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            var query = this.ConvertToQueryString(new Dictionary<string, object>() {
                { "allowInactiveAdmins", allowInactiveAdmins },
            });

            return await this.GetAsync<EnterpriseUsers>($"{this.EndpointUrl}/enterprise/{enterpriseId}/user" + query, cancellationToken);
        }

        /// <summary>
        /// Adds User To Enterprise
        /// </summary>
        /// <param name="enterpriseId">string /^[0-9a-f]{32}$/ Example: "585951a5df8380e0e3063e9f12345678"</param>
        /// <param name="username">The Username of the User that should be added to the Enterprise</param>
        /// <param name="usernames"></param>
        /// <param name="permission">Value:"admin"</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public WebCallResult<PendingApprovalInfo> AddUserToEnterprise(
            string enterpriseId,
            string username,
            string[] usernames,
            string permission,
            CancellationToken cancellationToken = default(CancellationToken))
            => this.AddUserToEnterpriseAsync(enterpriseId, username, usernames, permission, cancellationToken).Result;
        public async Task<WebCallResult<PendingApprovalInfo>> AddUserToEnterpriseAsync(
            string enterpriseId,
            string username,
            string[] usernames,
            string permission,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            var data = new RequestData_EnterpriseAddUser
            {
                Username = username,
                Usernames = usernames,
                Permission = permission,
            };

            return await this.PostAsync<PendingApprovalInfo>($"{this.EndpointUrl}/enterprise/{enterpriseId}/user", data, cancellationToken);
        }

        /// <summary>
        /// Removes User From Enterprise
        /// </summary>
        /// <param name="enterpriseId">string /^[0-9a-f]{32}$/ Example: "585951a5df8380e0e3063e9f12345678"</param>
        /// <param name="username">The Username of the User that should be removed from the Enterprise</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public WebCallResult<PendingApprovalInfo> RemoveUserFromEnterprise(
            string enterpriseId,
            string username,
            CancellationToken cancellationToken = default(CancellationToken))
            => this.RemoveUserFromEnterpriseAsync(enterpriseId, username, cancellationToken).Result;
        public async Task<WebCallResult<PendingApprovalInfo>> RemoveUserFromEnterpriseAsync(
            string enterpriseId,
            string username,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            var data = new RequestData_EnterpriseRemoveUser
            {
                Username = username,
            };

            return await this.DeleteAsync<PendingApprovalInfo>($"{this.EndpointUrl}/enterprise/{enterpriseId}/user", data, cancellationToken);
        }

        /// <summary>
        /// Freezes the enterprise for limited time
        /// </summary>
        /// <param name="enterpriseId">string /^[0-9a-f]{32}$/ Example: "585951a5df8380e0e3063e9f12345678"</param>
        /// <param name="durationInSeconds">seconds to freeze the enterprise for</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public WebCallResult<Freeze> FreezeEnterprise(
            string enterpriseId,
            int durationInSeconds,
            CancellationToken cancellationToken = default(CancellationToken))
            => this.FreezeEnterpriseAsync(enterpriseId, durationInSeconds, cancellationToken).Result;
        public async Task<WebCallResult<Freeze>> FreezeEnterpriseAsync(
            string enterpriseId,
            int durationInSeconds,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            var data = new RequestData_EnterpriseFreeze
            {
                Duration = durationInSeconds,
            };

            return await this.PostAsync<Freeze>($"{this.EndpointUrl}/enterprise/{enterpriseId}/freeze", data, cancellationToken);
        }

        /// <summary>
        /// Gets enterprise's wallet limits
        /// </summary>
        /// <param name="enterpriseId">string /^[0-9a-f]{32}$/ Example: "585951a5df8380e0e3063e9f12345678"</param>
        /// <param name="coin">Filter by coin(s). Example: ["btc"]</param>
        /// <param name="isCustodial">Whether custodial limits should be returned</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public WebCallResult<EnterpriseWalletLimits> GetEnterpriseWalletLimits(
            string enterpriseId,
            string[] coin = null,
            bool? isCustodial = null,
            CancellationToken cancellationToken = default(CancellationToken))
            => this.GetEnterpriseWalletLimitsAsync(enterpriseId, coin, isCustodial, cancellationToken).Result;
        public async Task<WebCallResult<EnterpriseWalletLimits>> GetEnterpriseWalletLimitsAsync(
            string enterpriseId,
            string[] coin = null,
            bool? isCustodial = null,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            var query = this.ConvertToQueryString(new Dictionary<string, object>() {
                { "coin", "[\"" + string.Join("\",\"", coin) + "\"]" },
                { "isCustodial", isCustodial },
            });

            return await this.GetAsync<EnterpriseWalletLimits>($"{this.EndpointUrl}/enterprise/{enterpriseId}/walletLimits" + query, cancellationToken);
        }
        #endregion

        #region Key
        /// <summary>
        /// Lists Keys
        /// </summary>
        /// <param name="coin">Required. Example: "btc"</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public WebCallResult<KeyInfoList> GetKeys(
            string coin,
            CancellationToken cancellationToken = default(CancellationToken))
            => this.GetKeysAsync(coin, cancellationToken).Result;
        public async Task<WebCallResult<KeyInfoList>> GetKeysAsync(
            string coin,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            return await this.GetAsync<KeyInfoList>($"{this.EndpointUrl}/{coin}/key", cancellationToken);
        }

        /// <summary>
        /// Creates Key
        /// </summary>
        /// <param name="coin">Required. Example: "btc"</param>
        /// <param name="enterpriseId">string /^[0-9a-f]{32}$/ Example: "585951a5df8380e0e3063e9f12345678"</param>
        /// <param name="source">Enum:"backup" "bitgo" "cold" "user"</param>
        /// <param name="encryptedPrv">Private part of this key pair, encrypted with a passphrase that only the client knows. Required for all sources except "bitgo".</param>
        /// <param name="newFeeAddress">Create a new keychain instead of fetching enterprise key (only for Ethereum)</param>
        /// <param name="pub">Public part of this key pair. Required for all sources except "bitgo".</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public WebCallResult<KeyInfo> CreateKey(
            string coin,
            string enterpriseId,
            string source = "bitgo",
            string encryptedPrv = "",
            bool newFeeAddress = false,
            string pub = "",
            CancellationToken cancellationToken = default(CancellationToken))
            => this.CreateKeyAsync(coin, enterpriseId, source, encryptedPrv, newFeeAddress, pub, cancellationToken).Result;
        public async Task<WebCallResult<KeyInfo>> CreateKeyAsync(
            string coin,
            string enterpriseId = "",
            string source = "bitgo",
            string encryptedPrv = "",
            bool newFeeAddress = false,
            string pub = "",
            CancellationToken cancellationToken = default(CancellationToken))
        {
            var data = new RequestData_KeyCreate
            {
                Source = source,
                EnterpriseId = enterpriseId,
                EncryptedPrv = encryptedPrv,
                UseNewFeeAddress = newFeeAddress,
                Pub = pub,
            };

            return await this.PostAsync<KeyInfo>($"{this.EndpointUrl}/{coin}/key", data, cancellationToken);
        }

        /// <summary>
        /// Gets a Key
        /// </summary>
        /// <param name="coin">Required. Example: "btc"</param>
        /// <param name="keyId">Required. string /^[0-9a-f]{32}$/ Example: "585951a5df8380e0e3063e9f12345678"</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public WebCallResult<KeyInfo> GetKey(
            string coin,
            string keyId,
            CancellationToken cancellationToken = default(CancellationToken))
            => this.GetAddressAsync(coin, keyId, cancellationToken).Result;
        public async Task<WebCallResult<KeyInfo>> GetAddressAsync(
            string coin,
            string keyId,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            return await this.GetAsync<KeyInfo>($"{this.EndpointUrl}/{coin}/key/{keyId}", cancellationToken);
        }
        #endregion

        #region Pending Approvals
        // TODO: Needs to Live Test
        /// <summary>
        /// Gets all pending approvals
        /// </summary>
        /// <param name="enterpriseId">Filter by enterprise. string /^[0-9a-f]{32}$/ Example: "585951a5df8380e0e3063e9f12345678"</param>
        /// <param name="walletId">Filter by wallet. string /^[0-9a-f]{32}$/ Example: "585951a5df8380e0e3063e9f12345678"</param>
        /// <param name="coin">Filter by coin. Example: ["btc"]</param>
        /// <param name="state">Items Enum:"pending" "awaitingSignature" "pendingBitGoAdminApproval" "pendingFinalApproval". Filter by state. The default behavior is to return objects where state is awaitingSignature, pending, pendingBitGoAdminApproval, or pendingFinalApproval.</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public WebCallResult<PendingApprovalInfoList> GetPendingApprovals(
            string enterpriseId = null,
            string walletId = null,
            string[] coin = null,
            string[] state = null,
            CancellationToken cancellationToken = default(CancellationToken))
            => this.GetPendingApprovalsAsync(enterpriseId, walletId, coin, state, cancellationToken).Result;
        public async Task<WebCallResult<PendingApprovalInfoList>> GetPendingApprovalsAsync(
            string enterpriseId = null,
            string walletId = null,
            string[] coin = null,
            string[] state = null,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            var dict = new Dictionary<string, object>();
            dict.Add("enterpriseId", enterpriseId);
            dict.Add("walletId", walletId);
            if (coin != null) dict.Add("coin", "[\"" + string.Join("\",\"", coin) + "\"]");
            if (state != null) dict.Add("state", "[\"" + string.Join("\",\"", state) + "\"]");
            var query = this.ConvertToQueryString(dict);

            return await this.GetAsync<PendingApprovalInfoList>($"{this.EndpointUrl}/pendingApprovals" + query, cancellationToken);
        }

        // TODO: Needs to Live Test
        /// <summary>
        /// Gets a pending approval
        /// </summary>
        /// <param name="pendingId">string /^[0-9a-f]{32}$/ Example: "585951a5df8380e0e3063e9f12345678"</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public WebCallResult<PendingApprovalInfo> GetPendingApproval(
            string pendingId,
            CancellationToken cancellationToken = default(CancellationToken))
            => this.GetPendingApprovalAsync(pendingId, cancellationToken).Result;
        public async Task<WebCallResult<PendingApprovalInfo>> GetPendingApprovalAsync(
            string pendingId,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            return await this.GetAsync<PendingApprovalInfo>($"{this.EndpointUrl}/pendingApprovals/{pendingId}", cancellationToken);
        }

        // TODO: Needs to Live Test
        /// <summary>
        /// Updates the state of a pending approval to either approved or rejected. Pending approvals are designed to be managed through our web UI. Requests made using an authentication token are not allowed to approve requests. Instead of using pending approvals we recommend creating a webhook policy to do automated approval and denial of transactions.
        /// </summary>
        /// <param name="pendingId">string /^[0-9a-f]{32}$/ Example: "585951a5df8380e0e3063e9f12345678"</param>
        /// <param name="otp"></param>
        /// <param name="state">approved or rejected</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public WebCallResult<PendingApprovalInfo> UpdatePendingApproval(
            string pendingId,
            string otp,
            string state,
            CancellationToken cancellationToken = default(CancellationToken))
            => this.UpdatePendingApprovalAsync(pendingId, otp, state, cancellationToken).Result;
        public async Task<WebCallResult<PendingApprovalInfo>> UpdatePendingApprovalAsync(
            string pendingId,
            string otp,
            string state,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            var data = new RequestData_PendingApprovalUpdate
            {
                OTP = otp,
                State = state
            };

            return await this.PutAsync<PendingApprovalInfo>($"{this.EndpointUrl}/pendingApprovals/{pendingId}", data, cancellationToken);
        }
        #endregion

        #region User
        /// <summary>
        /// Returns the associated user
        /// </summary>
        /// <param name="userId">The user ID, email address, or me for the currently authenticated user</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public WebCallResult<UserInfoResponse> GetUser(
            string userId,
            CancellationToken cancellationToken = default(CancellationToken))
            => this.GetUserAsync(userId, cancellationToken).Result;
        public async Task<WebCallResult<UserInfoResponse>> GetUserAsync(
            string userId,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            var query = this.ConvertToQueryString(new Dictionary<string, object> {
                { "id",userId},
            });

            return await this.GetAsync<UserInfoResponse>($"{this.EndpointUrl}/user/{userId}" + query, cancellationToken);
        }

        /// <summary>
        /// Creates a short-lived (1 hour) access token for use with the API. The token must be specified to subsequent API calls via the Authorization HTTP header:
        /// Authorization: Bearer 9b72c68ef394f5146f0f3efc1feafb7a971752cb00e79fafcfd8c1d2db83639c
        /// We don't recommend using this endpoint for scripting. The preferred approach is to create a long-lived token in the web UI (see the Developer Options section in User Settings).
        /// </summary>
        /// <param name="email">Email</param>
        /// <param name="password">Password</param>
        /// <param name="otp">Second factor authentication token</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public WebCallResult<UserLoginInfo> Login(
            string email,
            string password,
            string otp="",
            CancellationToken cancellationToken = default(CancellationToken))
            => this.LoginAsync(email, password, otp, cancellationToken).Result;
        public async Task<WebCallResult<UserLoginInfo>> LoginAsync(
            string email,
            string password,
            string otp="",
            CancellationToken cancellationToken = default(CancellationToken))
        {
            var data = new RequestData_UserLogin
            {
                Email=email,
                Password=password,
                OTP = otp,
            };

            return await this.PostAsync<UserLoginInfo>($"{this.EndpointUrl}/user/login", data, cancellationToken);
        }

        /// <summary>
        /// Disables an access token
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public WebCallResult<object> Logout(
            CancellationToken cancellationToken = default(CancellationToken))
            => this.LogoutAsync(cancellationToken).Result;
        public async Task<WebCallResult<object>> LogoutAsync(
            CancellationToken cancellationToken = default(CancellationToken))
        {
            return await this.GetAsync<object>($"{this.EndpointUrl}/user/logout", cancellationToken);
        }

        /// <summary>
        /// Returns the session associated with access token passed via the Authorization header.
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public WebCallResult<UserSession> GetSession(
            CancellationToken cancellationToken = default(CancellationToken))
            => this.GetSessionAsync(cancellationToken).Result;
        public async Task<WebCallResult<UserSession>> GetSessionAsync(
            CancellationToken cancellationToken = default(CancellationToken))
        {
            return await this.GetAsync<UserSession>($"{this.EndpointUrl}/user/session", cancellationToken);
        }

        /// <summary>
        /// Locks the current user session. This disallows operations that require an unlocked token, such as sending a transaction.
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public WebCallResult<UserSession> LockSession(
            CancellationToken cancellationToken = default(CancellationToken))
            => this.LockSessionAsync(cancellationToken).Result;
        public async Task<WebCallResult<UserSession>> LockSessionAsync(
            CancellationToken cancellationToken = default(CancellationToken))
        {
            return await this.PostAsync<UserSession>($"{this.EndpointUrl}/user/lock", cancellationToken);
        }

        /// <summary>
        /// Unlocks thes current user session. This allows operations that require an unlocked token, such as sending a transaction. Call this endpoint when an API returns a 401 response with the needsUnlock body parameter set to true.
        /// </summary>
        /// <param name="otp">Second factor authentication token</param>
        /// <param name="duration">integer [ 1 .. 3600 ]</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public WebCallResult<UserSession> UnlockSession(
            string otp = "",
            int duration = 600,
            CancellationToken cancellationToken = default(CancellationToken))
            => this.UnlockSessionAsync(otp,duration,cancellationToken).Result;
        public async Task<WebCallResult<UserSession>> UnlockSessionAsync(
            string otp = "",
            int duration = 600,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            var data = new RequestData_UserUnlockSession
            {
                OTP = otp,
                Duration = duration
            };

            return await this.PostAsync<UserSession>($"{this.EndpointUrl}/user/unlock", data, cancellationToken);
        }
        #endregion

        #region Transfers
        /// <summary>
        /// Returns deposits and withdrawals for a wallet. Transfers are sorted in descending order by height, then id.
        /// </summary>
        /// <param name="coin">Required. Example: "btc"</param>
        /// <param name="walletId">Required. string /^[0-9a-f]{32}$/ Example: "585951a5df8380e0e3063e9f12345678</param>
        /// <param name="allTokens">Include data for all ERC20 tokens</param>
        /// <param name="prevId">string /^[0-9a-f]{32}$/ Example: "585951a5df8380e0e3063e9f12345678. Return the next batch of results, based on the nextBatchPrevId value from the previous batch.</param>
        /// <param name="state">Enum. The status of this Transfer. Enum:"signed" "unconfirmed" "confirmed" "pendingApproval" "removed" "failed" "rejected"</param>
        /// <param name="searchLabel">Query for Transfers containing this string. Example: "3BAMY2UAudoEwucfwkg8sGR2FJHLPJoWsc"</param>
        /// <param name="limit">Default: 25. Maximum number of results to return. If the result set is truncated, use the nextBatchPrevId value to get the next batch.</param>
        /// <param name="type">Enum:"send" "receive". Filter on sending or receiving Transfers.</param>
        /// <param name="pendingApprovalId">string /^[0-9a-f]{32}$/ Example: "585951a5df8380e0e3063e9f12345678. Filter for a transfer with a matching pendingApprovalId</param>
        /// <param name="valueGte">Return transfers with a value that is greater than or equal to the given number</param>
        /// <param name="valueLt">Return transfers with a value that is less than the given number</param>
        /// <param name="dateGte">Return transfers with a date that is greater than or equal to the given timestamp</param>
        /// <param name="dateLt">Return transfers with a date that is less than the given timestamp</param>
        /// <param name="address">Example: ["2MvrwRYBAuRtPTiZ5MyKg42Ke55W3fZJfZS"]. Return transfers with elements in entries that have an address field set to this value</param>
        /// <param name="includeHex">Include the raw hex data of the transaction in the response (this may be a large amount of data)</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public WebCallResult<TransferInfoList> GetTransfers(
            string coin,
            string walletId,
            bool? allTokens = null,
            string prevId = null,
            string state = null,
            string searchLabel = null,
            int limit = 25,
            string type = null,
            string pendingApprovalId = null,
            long? valueGte = null,
            long? valueLt = null,
            DateTime? dateGte = null,
            DateTime? dateLt = null,
            string address = null,
            bool? includeHex = null,
            CancellationToken cancellationToken = default(CancellationToken))
            => this.GetTransfersAsync(
            coin,
            walletId,
            allTokens,
            prevId,
            state,
            searchLabel,
            limit,
            type,
            pendingApprovalId,
            valueGte,
            valueLt,
            dateGte,
            dateLt,
            address,
            includeHex,
            cancellationToken).Result;
        public async Task<WebCallResult<TransferInfoList>> GetTransfersAsync(
            string coin,
            string walletId,
            bool? allTokens = null,
            string prevId = null,
            string state = null,
            string searchLabel = null,
            int limit = 25,
            string type = null,
            string pendingApprovalId = null,
            long? valueGte = null,
            long? valueLt = null,
            DateTime? dateGte = null,
            DateTime? dateLt = null,
            string address = null,
            bool? includeHex = null,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            if (limit < 1 || limit > 500)
                return WebCallResult<TransferInfoList>.CreateErrorResult(new ArgumentError("Limit should be between 1-500"));

            var query = this.ConvertToQueryString(new Dictionary<string, object>() {
                { "allTokens", allTokens },
                { "prevId", prevId },
                { "state", state },
                { "searchLabel", searchLabel },
                { "limit", limit },
                { "type", type },
                { "pendingApprovalId", pendingApprovalId },
                { "valueGte", valueGte },
                { "valueLt", valueLt },
                { "dateGte", dateGte },
                { "dateLt", dateLt },
                { "address", address },
                { "includeHex", includeHex },
            });

            return await this.GetAsync<TransferInfoList>($"{this.EndpointUrl}/{coin}/wallet/{walletId}/transfer" + query, cancellationToken);
        }

        /// <summary>
        /// Returns transfer details
        /// </summary>
        /// <param name="coin">Required. Example: "btc"</param>
        /// <param name="walletId">Required. string /^[0-9a-f]{32}$/ Example: "585951a5df8380e0e3063e9f12345678</param>
        /// <param name="transferId">a transfer or transaction id. Example: "f5d8ee39a430901c91a5917b9f2dc19d6d1a0e9cea205b009ca73dd04470b9a5 or 585951a5df8380e0e3063e9f12345678"</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public WebCallResult<TransferInfo> GetTransfer(
            string coin,
            string walletId,
            string transferId,
            CancellationToken cancellationToken = default(CancellationToken))
            => this.GetTransferAsync(coin,walletId, transferId, cancellationToken).Result;
        public async Task<WebCallResult<TransferInfo>> GetTransferAsync(
            string coin,
            string walletId,
            string transferId = null,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            return await this.GetAsync<TransferInfo>($"{this.EndpointUrl}/{coin}/wallet/{walletId}/transfer/{transferId}", cancellationToken);
        }

        /// <summary>
        /// Returns transfer details
        /// </summary>
        /// <param name="coin">Required. Example: "btc"</param>
        /// <param name="walletId">Required. string /^[0-9a-f]{32}$/ Example: "585951a5df8380e0e3063e9f12345678</param>
        /// <param name="sequenceId"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public WebCallResult<TransferInfo> GetTransferBySequenceId(
            string coin,
            string walletId,
            string sequenceId,
            CancellationToken cancellationToken = default(CancellationToken))
            => this.GetTransferBySequenceIdAsync(coin, walletId, sequenceId, cancellationToken).Result;
        public async Task<WebCallResult<TransferInfo>> GetTransferBySequenceIdAsync(
            string coin,
            string walletId,
            string sequenceId = null,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            return await this.GetAsync<TransferInfo>($"{this.EndpointUrl}/{coin}/wallet/{walletId}/transfer/sequenceId/{sequenceId}", cancellationToken);
        }

        /// <summary>
        /// Returns the estimated fee for a transaction. UTXO coins will return a fee per kB, while Account-based coins will return a flat fee estimate
        /// </summary>
        /// <param name="coin">Required. Example: "btc"</param>
        /// <param name="numBlocks">target number of blocks</param>
        /// <param name="recipient">Recipient of the tx to estimate for (only for ETH)</param>
        /// <param name="data">ETH data of the tx to estimate for (only for ETH)</param>
        /// <param name="hop">True if we are estimating for a hop tx, false or unspecified for a wallet tx (only for ETH)</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public WebCallResult<FeeEstimateInfo> FeeEstimate(
            string coin,
            int? numBlocks=null,
            string recipient=null,
            string data=null,
            bool? hop =null,
            CancellationToken cancellationToken = default(CancellationToken))
            => this.FeeEstimateAsync(coin, numBlocks, recipient, data, hop, cancellationToken).Result;
        public async Task<WebCallResult<FeeEstimateInfo>> FeeEstimateAsync(
            string coin,
            int? numBlocks = null,
            string recipient = null,
            string data = null,
            bool? hop = null,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            var query = this.ConvertToQueryString(new Dictionary<string, object>() {
                { "numBlocks", numBlocks },
                { "recipient", recipient },
                { "data", data },
                { "hop", hop },
            });

            return await this.GetAsync<FeeEstimateInfo>($"{this.EndpointUrl}/{coin}/tx/fee" + query, cancellationToken);
        }
        #endregion

        #region Wallets
        /// <summary>
        /// List wallets
        /// </summary>
        /// <param name="coin">Required. Example: "btc". Lowercase coin symbol</param>
        /// <param name="limit">integer [ 1 .. 500 ]. Default: 25. Maximum number of results to return. If the result set is truncated, use the nextBatchPrevId value to get the next batch.</param>
        /// <param name="allTokens">Example: true. Include data for all ERC20 tokens</param>
        /// <param name="prevId">Example: "585951a5df8380e0e3063e9f". Return the next batch of results, based on the nextBatchPrevId value from the previous batch.</param>
        /// <param name="searchLabel">Example: "3BAMY2UAudoEwucfwkg8sGR2FJHLPJoWsc". Query for Transfers containing this string</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public WebCallResult<WalletInfoList> GetWallets(
            string coin,
            int limit = 25,
            bool allTokens = true,
            string prevId = null,
            string searchLabel = null,
            CancellationToken cancellationToken = default(CancellationToken))
            => this.GetWalletsAsync(coin, limit, allTokens, prevId, searchLabel, cancellationToken).Result;
        public async Task<WebCallResult<WalletInfoList>> GetWalletsAsync(
            string coin,
            int limit = 25,
            bool allTokens = true,
            string prevId = null,
            string searchLabel = null,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            if (limit < 1 || limit > 500)
                return WebCallResult<WalletInfoList>.CreateErrorResult(new ArgumentError("Limit should be between 1-500"));

            var query = this.ConvertToQueryString(new Dictionary<string, object>() {
                { "prevId", prevId },
                { "limit", limit },
                { "allTokens", allTokens },
                { "searchLabel", searchLabel }
            });

            return await this.GetAsync<WalletInfoList>($"{this.EndpointUrl}/{coin}/wallet" + query, cancellationToken);
        }

        /// <summary>
        /// Gets wallet details
        /// </summary>
        /// <param name="coin">Example: "btc"</param>
        /// <param name="walletId">string /^[0-9a-f]{32}$/ Example: "585951a5df8380e0e3063e9f12345678"</param>
        /// <param name="allTokens">Include data for all ERC20 tokens</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public WebCallResult<WalletInfo> GetWallet(
            string coin,
            string walletId = null,
            bool? allTokens = null,
            CancellationToken cancellationToken = default(CancellationToken))
            => this.GetWalletsAsync(coin, walletId, allTokens, cancellationToken).Result;
        public async Task<WebCallResult<WalletInfo>> GetWalletsAsync(
            string coin,
            string walletId = null,
            bool? allTokens = null,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            var query = this.ConvertToQueryString(new Dictionary<string, object>() {
                { "allTokens", allTokens },
            });

            return await this.GetAsync<WalletInfo>($"{this.EndpointUrl}/{coin}/wallet/{walletId}" + query, cancellationToken);
        }

        /// <summary>
        /// Gets wallet details by address
        /// </summary>
        /// <param name="coin">Example: "btc"</param>
        /// <param name="address">Coin Wallet Address. Example: "2MvrwRYBAuRtPTiZ5MyKg42Ke55W3fZJfZS"</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public WebCallResult<WalletInfo> GetWalletByAddress(
            string coin,
            string address,
            CancellationToken cancellationToken = default(CancellationToken))
            => this.GetWalletByAddressAsync(coin, address, cancellationToken).Result;
        public async Task<WebCallResult<WalletInfo>> GetWalletByAddressAsync(
            string coin,
            string address,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            return await this.GetAsync<WalletInfo>($"{this.EndpointUrl}/{coin}/wallet/address/{address}", cancellationToken);
        }









        /*
        public WebCallResult<WalletInfo> GenerateWallet(
            string coin,
            string label,
            string passphrase,
            string userKey ,
            string backupXpub ,
            string backupXpubProvider ,
            string enterpriseId ,
            bool disableTransactionNotifications ,
            string passcodeEncryptionCode ,
            string coldDerivationSeed ,
            int gasPrice,
            bool disableKRSEmail,
            CancellationToken cancellationToken = default(CancellationToken))
            => this.GenerateWalletAsync(
            coin, 
            label,
            passphrase, 
            userKey, 
            backupXpub, 
            backupXpubProvider, 
            enterpriseId, 
            disableTransactionNotifications,
            passcodeEncryptionCode,
            coldDerivationSeed,
            gasPrice,
            disableKRSEmail,
            cancellationToken).Result;
        public async Task<WebCallResult<WalletInfo>> GenerateWalletAsync(
            string coin,
            string label,
            string passphrase,
            string userKey,
            string backupXpub,
            string backupXpubProvider,
            string enterpriseId,
            bool disableTransactionNotifications,
            string passcodeEncryptionCode,
            string coldDerivationSeed,
            int gasPrice,
            bool disableKRSEmail,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            var data = new RequestData_GenerateWallet
            {
                Label = label,
                Passphrase = passphrase,
                UserKey = userKey,
                BackupXpub = backupXpub,
                BackupXpubProvider = backupXpubProvider,
                EnterpriseId = enterpriseId,
                DisableTransactionNotifications = disableTransactionNotifications,
                PasscodeEncryptionCode = passcodeEncryptionCode,
                ColdDerivationSeed = coldDerivationSeed,
                GasPrice = gasPrice,
                DisableKRSEmail = disableKRSEmail
            };

            return await this.PostAsync<WalletInfo>($"{this.EndpointUrl}/{coin}/wallet/generate", data, cancellationToken);
        }
        */



        // TODO: Need Live Test
        /// <summary>
        /// This method is for advanced API users and allows manual creation and specification of keys. In the SDK or BitGo Express, Generate Wallet is the simpler and highly recommended method to create a wallet. Another option is to create your wallets in our UI.
        /// This API creates a new wallet for the user or enterprise.The keys to use with the new wallet(passed in the 'keys' parameter) must be registered with BitGo prior to using this API.
        /// BitGo currently only supports 2-of-3 (e.g.m= 2 and n = 3) wallets. The third key, and only the third key, must be a BitGo key. The first key is by convention the user key, with its encrypted xprv stored on BitGo.
        /// Ethereum and XRP wallets can only be created under an enterprise. Pass in the id of the enterprise to associate the wallet with. Your enterprise id can be seen by clicking on the "Manage Organization" link in the enterprise dropdown. Using the Add Wallet API, you can create a wallet using either the enterprise fee address(used by default for all wallets in the enterprise), or a unique fee address(created manually with the Keychains API). Pass the desired key as the third key ID in the 'keys' array.In either case, the fee address must be funded before creating the wallet.
        /// You cannot generate a wallet by passing in an ERC20 token as the coin. ERC20 tokens share Ethereum wallets and it is not possible to create a wallet specific to one token.
        /// BitGo Ethereum wallet is a smart-contract implementing multi-signature scheme. Because contracts itself can not initiate transactions, fee addresses are used for this purpose.Ethereum transactions initiated by a given address, are confirmed by the network in order of creation, so one lower fee transaction can potentially delay all subsequent transactions.To help lower network fee costs, two fee addresses are provided.
        /// feeAddress is a main fee address usable for all operations. lowPriorityFeeAddress is a secondary fee address that can be used to pay lower fee for Create Address operations without risking delaying subsequent higher-priority transactions initiated by main fee address.
        /// </summary>
        /// <param name="coin">Example: "btc"</param>
        /// <param name="label"></param>
        /// <param name="enterpriseId">string /^[0-9a-f]{32}$/ Example: "585951a5df8380e0e3063e9f12345678"</param>
        /// <param name="stellarUsername">Username for the user's Stellar address. It's case insensitive, and it can't be changed after it's set.</param>
        /// <param name="isCold"></param>
        /// <param name="keys"></param>
        /// <param name="bitgoKeySignature">a signature of the bitgo pub key using the user key (useful for change address verification)</param>
        /// <param name="backupKeySignature">a signature of the backup pub key using the user key (useful for change address verification)</param>
        /// <param name="requiredSignaturesNumber">Number of signatures required. This value must be 2 for hot wallets, 1 for ofc wallets, and not specified for custodial wallets.</param>
        /// <param name="providedKeysNumber">Number of keys provided. This value must be 3 for hot wallets, 1 for ofc wallets, and not specified for custodial wallets.</param>
        /// <param name="tags"></param>
        /// <param name="type">Enum:"custodial" "custodialPaired". The type describes who owns the keys to the wallet and how they are stored. custodial means that this wallet is a cold wallet where BitGo owns the keys. Only customers of the BitGo Trust can create this kind of wallet. custodialPaired means that this is a hot wallet that is owned by the customer but it will be linked to a cold (custodial) wallet where BitGo owns the keys. This option is only available to customers of BitGo Inc.</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public WebCallResult<WalletInfo> AddWallet(
            string coin,
            string label,
            string enterpriseId,
            string stellarUsername,
            bool isCold,
            string[] keys,
            string bitgoKeySignature,
            string backupKeySignature,
            int requiredSignaturesNumber,
            int providedKeysNumber,
            string[] tags,
            string type,
            CancellationToken cancellationToken = default(CancellationToken))
            => this.AddWalletAsync(
            coin,
            label,
            enterpriseId,
            stellarUsername,
            isCold,
            keys,
            bitgoKeySignature,
            backupKeySignature,
            requiredSignaturesNumber,
            providedKeysNumber,
            tags,
            type,
            cancellationToken).Result;
        public async Task<WebCallResult<WalletInfo>> AddWalletAsync(
            string coin,
            string label,
            string enterpriseId,
            string stellarUsername,
            bool isCold,
            string[] keys,
            string bitgoKeySignature,
            string backupKeySignature,
            int requiredSignaturesNumber,
            int providedKeysNumber,
            string[] tags,
            string type,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            var data = new RequestData_AddWallet
            {
                CoinSpecific =  new Dictionary<string, Dictionary<string, string>>
                {
                    {
                        "xlm", new Dictionary<string, string>
                        {
                            { "stellarUsername", stellarUsername}
                        }
                    },
                    {
                        "txlm", new Dictionary<string, string>
                        {
                            { "stellarUsername", stellarUsername}
                        }
                    },
                } ,
                EnterpriseId = enterpriseId,
                IsCold = isCold,
                Keys = keys,
                KeySignatures = new Dictionary<string, string>
                {
                    {"bitgo",bitgoKeySignature },
                    {"backup",backupKeySignature }
                },
                Label = label,
                RequiredSignaturesNumber = requiredSignaturesNumber,
                ProvidedKeysNumber = providedKeysNumber,
                Tags = tags,
                Type = type
            };

            return await this.PostAsync<WalletInfo>($"{this.EndpointUrl}/{coin}/wallet", data, cancellationToken);
        }

        /// <summary>
        /// Updates wallet
        /// </summary>
        /// <param name="coin">Example: "btc"</param>
        /// <param name="address">Coin Wallet Address. Example: "2MvrwRYBAuRtPTiZ5MyKg42Ke55W3fZJfZS"</param>
        /// <param name="walletLabel"></param>
        /// <param name="approvalsRequired">integer >= 1</param>
        /// <param name="disableTransactionNotifications"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public WebCallResult<WalletInfo> UpdateWallet(
            string coin,
            string walletId,
            string walletLabel,
            int approvalsRequired,
            bool disableTransactionNotifications,
            CancellationToken cancellationToken = default(CancellationToken))
            => this.UpdateWalletAsync(coin, walletId, walletLabel, approvalsRequired, disableTransactionNotifications, cancellationToken).Result;
        public async Task<WebCallResult<WalletInfo>> UpdateWalletAsync(
            string coin,
            string walletId,
            string walletLabel,
            int approvalsRequired,
            bool disableTransactionNotifications,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            var data = new RequestData_WalletUpdate
            {
                Label = walletLabel,
                ApprovalsRequired = approvalsRequired,
                DisableTransactionNotifications = disableTransactionNotifications
            };

            return await this.PutAsync<WalletInfo>($"{this.EndpointUrl}/{coin}/wallet/{walletId}", data, cancellationToken);
        }

        /// <summary>
        /// You will no longer see or have access to this wallet, but it remains accessible to other wallet users.
        /// If you are the only user on the wallet, the wallet must have a 0 balance.
        /// </summary>
        /// <param name="coin">Example: "btc"</param>
        /// <param name="address">Coin Wallet Address. Example: "2MvrwRYBAuRtPTiZ5MyKg42Ke55W3fZJfZS"</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public WebCallResult<WalletInfo> DeleteWallet(
            string coin,
            string walletId,
            CancellationToken cancellationToken = default(CancellationToken))
            => this.DeleteWalletAsync(coin, walletId, cancellationToken).Result;
        public async Task<WebCallResult<WalletInfo>> DeleteWalletAsync(
            string coin,
            string walletId,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            return await this.DeleteAsync<WalletInfo>($"{this.EndpointUrl}/{coin}/wallet/{walletId}", null, cancellationToken);
        }

        // TODO: Need Live Test
        /// <summary>
        /// After a user has accepted a wallet share, they become a party on a wallet and the wallet share is considered “complete”. In order to revoke the share after they have accepted, you can remove the user from the wallet.
        /// This operation requires approval by another wallet administrator if there is more than a single administrator on a wallet.
        /// </summary>
        /// <param name="coin">Example: "btc"</param>
        /// <param name="walletId">string /^[0-9a-f]{32}$/ Example: "585951a5df8380e0e3063e9f12345678"</param>
        /// <param name="userId">string /^[0-9a-f]{32}$/ Example: "585951a5df8380e0e3063e9f12345678"</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public WebCallResult<WalletInfo> RemoveUserFromWallet(
            string coin,
            string walletId,
            string userId,
            CancellationToken cancellationToken = default(CancellationToken))
            => this.RemoveUserFromWalletAsync(coin, walletId, userId, cancellationToken).Result;
        public async Task<WebCallResult<WalletInfo>> RemoveUserFromWalletAsync(
            string coin,
            string walletId,
            string userId,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            return await this.DeleteAsync<WalletInfo>($"{this.EndpointUrl}/{coin}/wallet/{walletId}/user/{userId}", null, cancellationToken);
        }

        /// <summary>
        /// Freezes the enterprise for limited time
        /// </summary>
        /// <param name="coin">Example: "btc"</param>
        /// <param name="walletId">string /^[0-9a-f]{32}$/ Example: "585951a5df8380e0e3063e9f12345678"</param>
        /// <param name="durationInSeconds">seconds to freeze the enterprise for</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public WebCallResult<Freeze> FreezeWallet(
            string coin,
            string walletId,
            int durationInSeconds,
            CancellationToken cancellationToken = default(CancellationToken))
            => this.FreezeWalletAsync(coin, walletId, durationInSeconds, cancellationToken).Result;
        public async Task<WebCallResult<Freeze>> FreezeWalletAsync(
            string coin,
            string walletId,
            int durationInSeconds,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            var data = new RequestData_WalletFreeze
            {
                Duration = durationInSeconds,
            };

            return await this.PostAsync<Freeze>($"{this.EndpointUrl}/{coin}/wallet/{walletId}/freeze", data, cancellationToken);
        }

        /// <summary>
        /// Get the total balance, confirmed balance, and spendable balance of the wallets of a certain coin type
        /// </summary>
        /// <param name="coin">Example: "btc"</param>
        /// <param name="allTokens">Include data for all ERC20 tokens</param>
        /// <param name="enterpriseId">string /^[0-9a-f]{32}$/ Example: "585951a5df8380e0e3063e9f12345678"</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public WebCallResult<BalanceInfo> GetBalances(
            string coin,
            bool allTokens = true,
            string enterpriseId = null,
            CancellationToken cancellationToken = default(CancellationToken))
            => this.GetBalancesAsync(coin, allTokens, enterpriseId, cancellationToken).Result;
        public async Task<WebCallResult<BalanceInfo>> GetBalancesAsync(
            string coin,
            bool allTokens = true,
            string enterpriseId = null,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            var query = this.ConvertToQueryString(new Dictionary<string, object>() {
                { "allTokens", allTokens },
                { "enterprise", enterpriseId },
            });

            return await this.GetAsync<BalanceInfo>($"{this.EndpointUrl}/{coin}/wallet/balances" + query, cancellationToken);
        }

        /// <summary>
        /// Returns information about reserve requirements for an account. Currently only available for Stellar.
        /// </summary>
        /// <param name="coin">Enum:"txlm" "xlm"</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public WebCallResult<BalanceReserve> GetBalanceReserve(
            string coin,
            CancellationToken cancellationToken = default(CancellationToken))
            => this.GetBalanceReserveAsync(coin, cancellationToken).Result;
        public async Task<WebCallResult<BalanceReserve>> GetBalanceReserveAsync(
            string coin,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            return await this.GetAsync<BalanceReserve>($"{this.EndpointUrl}/{coin}/requiredReserve" , cancellationToken);
        }

        /// <summary>
        /// Returns unspect transaction outputs for a wallet
        /// </summary>
        /// <param name="coin">Example: "btc"</param>
        /// <param name="walletId">string /^[0-9a-f]{32}$/ Example: "585951a5df8380e0e3063e9f12345678"</param>
        /// <param name="limit">integer [ 1 .. 500 ]. Default: 25. Maximum number of results to return. If the result set is truncated, use the nextBatchPrevId value to get the next batch.</param>
        /// <param name="minValue">number >= 0. Minimum value of each unspent in base units(e.g.satoshis)</param>
        /// <param name="maxValue">number >= 0. Maximum value of each unspent in base units(e.g.satoshis)</param>
        /// <param name="minConfirms">integer >= 0. Minimum number of confirmation for the collected inputs</param>
        /// <param name="minHeight">number >= 0. Minimum block height of the unspents</param>
        /// <param name="prevId">Example: "585951a5df8380e0e3063e9f". Return the next batch of results, based on the nextBatchPrevId value from the previous batch.</param>
        /// <param name="target">integer >= 0. Combined target value of the unspents</param>
        /// <param name="chains">Array of string. Items Enum:0 1 10 11 20 21. Replaces segwit. Mutually exclusive with segwit. Returns only unspents/addresses of the chains passed. If neither chains nor segwit is passed unspents/addresses from all chains are returned.</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public WebCallResult<UnspentsInfoList> GetUnspents(
            string coin,
            string walletId,
            int limit=25,
            decimal? minValue = null,
            decimal? maxValue=null,
            int? minConfirms = null,
            int? minHeight = null,
            string prevId = null,
            int? target = null,
            string[] chains = null,
            CancellationToken cancellationToken = default(CancellationToken))
            => this.GetUnspentsAsync(coin, walletId, limit, minValue, maxValue, minConfirms, minHeight, prevId, target, chains, cancellationToken).Result;
        public async Task<WebCallResult<UnspentsInfoList>> GetUnspentsAsync(
            string coin,
            string walletId,
            int limit = 25,
            decimal? minValue = null,
            decimal? maxValue = null,
            int? minConfirms = null,
            int? minHeight = null,
            string prevId = null,
            int? target = null,
            string[] chains = null,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            if (limit < 1 || limit > 500)
                return WebCallResult<UnspentsInfoList>.CreateErrorResult(new ArgumentError("Limit should be between 1-500"));

            var query = this.ConvertToQueryString(new Dictionary<string, object>() {
                { "limit", limit },
                { "minValue", minValue },
                { "maxValue", maxValue },
                { "minConfirms", minConfirms },
                { "minHeight", minHeight },
                { "prevId", prevId },
                { "target", target },
                { "chains", chains },
            });

            return await this.GetAsync<UnspentsInfoList>($"{this.EndpointUrl}/{coin}/wallet/{walletId}/unspents" + query, cancellationToken);
        }

        /// <summary>
        /// Returns the maximum amount that can be spent with a single transaction on the wallet.
        /// The maximum spendable amount can differ from a wallet’s total balance. A transaction can only use up to 200 unspents. Wallets that have more than 200 unspents cannot spend the full balance in one transaction. Additionally, the value returned for the maximum spendable amount accounts for the current fee level by deducting the estimated fees. The amount will only be calculated based on the unspents that fit the parameters passed.
        /// </summary>
        /// <param name="coin">Example: "btc"</param>
        /// <param name="walletId">string /^[0-9a-f]{32}$/ Example: "585951a5df8380e0e3063e9f12345678"</param>
        /// <param name="limit">integer [ 1 .. 500 ]. Default: 25. Maximum number of results to return. If the result set is truncated, use the nextBatchPrevId value to get the next batch.</param>
        /// <param name="allTokens">Include data for all ERC20 tokens</param>
        /// <param name="enforceMinConfirmsForChange">Enforces minConfirms on change inputs</param>
        /// <param name="feeRate">integer >= 0</param>
        /// <param name="maxFeeRate">integer >= 0</param>
        /// <param name="minConfirms">integer >= 0. Minimum number of confirmation for the collected inputs</param>
        /// <param name="minHeight">number >= 0. Minimum block height of the unspents</param>
        /// <param name="minValue">number >= 0. Minimum value of each unspent in base units (e.g. satoshis)</param>
        /// <param name="maxValue">number >= 0. Maximum value of each unspent in base units (e.g. satoshis)</param>
        /// <param name="numBlocks">integer [ 1 .. 1000 ]. Default: 2. Sets the target estimated number of blocks for a confirmation</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public WebCallResult<SpendableInfo> GetMaximumSpendable(
            string coin,
            string walletId,
            int limit = 25,
            bool? allTokens = null,
            bool? enforceMinConfirmsForChange = null,
            int? feeRate = null,
            int? maxFeeRate = null,
            int? minConfirms = null,
            decimal? minHeight = null,
            decimal? minValue = null,
            decimal? maxValue = null,
            int numBlocks = 2,
            CancellationToken cancellationToken = default(CancellationToken))
            => this.GetMaximumSpendableAsync(coin, walletId, limit, allTokens, enforceMinConfirmsForChange, feeRate, maxFeeRate, minConfirms, minHeight, minValue, maxValue, numBlocks, cancellationToken).Result;
        public async Task<WebCallResult<SpendableInfo>> GetMaximumSpendableAsync(
            string coin,
            string walletId,
            int limit = 25,
            bool? allTokens = null,
            bool? enforceMinConfirmsForChange = null,
            int? feeRate = null,
            int? maxFeeRate = null,
            int? minConfirms = null,
            decimal? minHeight = null,
            decimal? minValue = null,
            decimal? maxValue = null,
            int numBlocks = 2,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            if (limit < 1 || limit > 500)
                return WebCallResult<SpendableInfo>.CreateErrorResult(new ArgumentError("Limit should be between 1-500"));

            if (numBlocks < 1 || numBlocks > 1000)
                return WebCallResult<SpendableInfo>.CreateErrorResult(new ArgumentError("Limit should be between 1-1000"));

            var query = this.ConvertToQueryString(new Dictionary<string, object>() {
                { "limit", limit },
                { "allTokens", allTokens },
                { "enforceMinConfirmsForChange", enforceMinConfirmsForChange },
                { "feeRate", feeRate },
                { "maxFeeRate", maxFeeRate },
                { "minConfirms", minConfirms },
                { "minHeight", minHeight },
                { "minValue", minValue },
                { "maxValue", maxValue },
                { "numBlocks", numBlocks },
            });

            return await this.GetAsync<SpendableInfo>($"{this.EndpointUrl}/{coin}/wallet/{walletId}/maximumSpendable" + query, cancellationToken);
        }

















        // buradan devam

        // Build a transaction
        // Initiate a transaction
        // Send a half-signed transaction














        #endregion

        #region Private Methods
        private HttpClient GetHttpClient()
        {
            var handler = new HttpClientHandler()
            {
                AllowAutoRedirect = false
            };
            var client = new HttpClient(handler);
            client.BaseAddress = new Uri(this.EndpointUrl);
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", this.AccessToken.SecureStringToString());
            return client;
        }

        private bool CheckForErrors(string data)
        {
            return data.Contains("\"error\"") && data.Contains("\"name\"") && data.Contains("\"requestId\"");
        }

        private string ConvertToQueryString(Dictionary<string, object> nvc)
        {
            var array = nvc
                .Where(keyValue => keyValue.Value != null)
                .Select(keyValue => new KeyValuePair<string, string>(keyValue.Key, keyValue.Value.ConvertValueToString()))
                .Select(keyValue => $"{WebUtility.UrlEncode(keyValue.Key)}={WebUtility.UrlEncode(keyValue.Value)}")
                .ToArray();
            return array.Any() ? "?" + string.Join("&", array) : string.Empty;
        }

        private async Task<WebCallResult<T>> GetAsync<T>(string url, CancellationToken cancellationToken = default(CancellationToken))
        {
            using (var client = this.GetHttpClient())
            {
                var response = await client.GetAsync($"{url}", cancellationToken);
                var content = await response.Content.ReadAsStringAsync();

                // Return
                return this.EvaluateResponse<T>(response, content);
            }
        }

        private async Task<WebCallResult<T>> PostAsync<T>(string url, object obj = null, CancellationToken cancellationToken = default(CancellationToken)) 
        {
            using (var client = GetHttpClient())
            {
                var data = JsonConvert.SerializeObject(obj ?? new object());
                var response = await client.PostAsync($"{url}", new StringContent(data, Encoding.UTF8, "application/json"), cancellationToken);
                var content = await response.Content.ReadAsStringAsync();

                // Return
                return this.EvaluateResponse<T>(response, content);
            }
        }

        private async Task<WebCallResult<T>> PutAsync<T>(string url, object obj = null, CancellationToken cancellationToken = default(CancellationToken))
        {
            using (var client = GetHttpClient())
            {
                var data = JsonConvert.SerializeObject(obj ?? new object());
                var response = await client.PutAsync($"{url}", new StringContent(data, Encoding.UTF8, "application/json"), cancellationToken);
                var content = await response.Content.ReadAsStringAsync();

                // Return
                return this.EvaluateResponse<T>(response, content);
            }
        }

        private async Task<WebCallResult<T>> DeleteAsync<T>(string url, object obj = null, CancellationToken cancellationToken = default(CancellationToken))
        {
            using (var client = GetHttpClient())
            {
                var request = new HttpRequestMessage(HttpMethod.Delete, $"{url}");
                var data = JsonConvert.SerializeObject(obj ?? new object());
                request.Content = new StringContent(data, Encoding.UTF8, "application/json");
                var response = await client.SendAsync(request, cancellationToken);
                var content = await response.Content.ReadAsStringAsync();

                // Return
                return this.EvaluateResponse<T>(response, content);
            }
        }

        private WebCallResult<T> EvaluateResponse<T>(HttpResponseMessage response, string content)
        {
            // Error
            Error error = null;
            if (this.CheckForErrors(content))
            {
                error = JsonConvert.DeserializeObject<Error>(content);
            }

            // Data
            else
            {
                if (string.IsNullOrEmpty(content))
                {
                    error = new Error("000", "Empty Response", "");
                }

                // Return
                return new WebCallResult<T>(response, JsonConvert.DeserializeObject<T>(content), error);
            }

            // Http Status Codes
            if (error == null)
            {
                // 200
                if (response.StatusCode == HttpStatusCode.OK) { }
                // 202
                else if (response.StatusCode == HttpStatusCode.Accepted) { }
                // 206
                else if (response.StatusCode == HttpStatusCode.PartialContent) { }
                // 400
                else if (response.StatusCode == HttpStatusCode.BadRequest) { error = new Error(((int)response.StatusCode).ToString(), response.StatusCode.ToString(), ""); }
                // 401
                else if (response.StatusCode == HttpStatusCode.Unauthorized) { error = new Error(((int)response.StatusCode).ToString(), response.StatusCode.ToString(), ""); }
                // 403
                else if (response.StatusCode == HttpStatusCode.Forbidden) { error = new Error(((int)response.StatusCode).ToString(), response.StatusCode.ToString(), ""); }
                // 404
                else if (response.StatusCode == HttpStatusCode.NotFound) { error = new Error(((int)response.StatusCode).ToString(), response.StatusCode.ToString(), ""); }
                else { error = new Error(((int)response.StatusCode).ToString(), response.StatusCode.ToString(), ""); }
            }

            // Return Default
            return new WebCallResult<T>(response, default(T), error);
        }
        #endregion

    }
}
