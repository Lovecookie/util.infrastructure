

using PlayFab.MultiplayerModels;

namespace Util.Infrastructure.Azure;

public class PlayfabAgent
{   
    private readonly IConfiguration _configuration;
    private readonly ILogger<PlayfabAgent> _logger;
    private string _titleID = "";
    private string _secretKey = "";
    private string _buildID = "";
    private string _entityToken = "";
    

    public PlayfabAgent(IConfiguration configuration, ILogger<PlayfabAgent> logger)
    {
        _configuration = configuration;
        _logger = logger;

        _titleID = _configuration["PF_TITLEID"] ?? "";
        _secretKey = _configuration["PF_SECRET"] ?? "";
        _buildID = _configuration["PF_BUILDID"] ?? "";

        PlayFabSettings.staticSettings.TitleId = _titleID;
        PlayFabSettings.staticSettings.DeveloperSecretKey = _secretKey;
    }

    public async Task<TOutcome<string>> TryGetEntityToken()
    {
        if(_entityToken != "")
        {
            return TOutcome.Ok(_entityToken);
        }

        var req = new PlayFab.AuthenticationModels.GetEntityTokenRequest();

        var res = await PlayFabAuthenticationAPI.GetEntityTokenAsync(req);
        if(res.Error != null)
        {
            _logger.LogError("Error getting entity token: {0}", res.Error.ErrorMessage);
            return TOutcome.Err<string>(res.Error.ErrorMessage);
        }

        _entityToken = res.Result.EntityToken;

        return TOutcome.Ok(_entityToken);
    }

    public async Task<TOutcome<RequestMultiplayerServerResponse>> TryGetMultiplayerServer(string sessionID, string accountID)
    {
        var req = new PlayFab.MultiplayerModels.RequestMultiplayerServerRequest();
        req.BuildId = _buildID;

        var regionsOutcome = await TryGetRegions();
        if (false == regionsOutcome.Success)
        {
            return TOutcome.Err<RequestMultiplayerServerResponse>(regionsOutcome.Message);
        }

        var regions = regionsOutcome.Value!;
        req.PreferredRegions = new List<string>();
        req.SessionId = sessionID;
        regions.ToList().ForEach(x => req.PreferredRegions.Add(x));

        req.InitialPlayers ??= new List<string>();
        req.InitialPlayers.Add(accountID);

        var res = await PlayFabMultiplayerAPI.RequestMultiplayerServerAsync(req);
        if (res.Error != null)
        {
            return TOutcome.Err<RequestMultiplayerServerResponse>(res.Error.ErrorMessage);
        }

        return TOutcome.Ok(res.Result);
    }

    public async Task<TOutcome<IEnumerable<string>>> TryGetRegions()
    {
        var req = new PlayFab.MultiplayerModels.GetBuildRequest();
        req.BuildId = _buildID;

        var res = await PlayFabMultiplayerAPI.GetBuildAsync(req);
        if(res.Error != null)
        {
            return TOutcome.Err<IEnumerable<string>>(res.Error.ErrorMessage);
        }

        var region = res.Result.RegionConfigurations.Select( x => x.Region);
        if (region.Count() == 0)
        {
            return TOutcome.Err<IEnumerable<string>>("No region found");
        }

        return TOutcome.Ok(region);
    }
}
