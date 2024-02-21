﻿namespace Util.Infrastructure;

public class JwtFields
{
	public string Secret { get; set; } = "";
	public string Audience { get; set; } = "";
	public string Issuer { get; set; } = "";
}