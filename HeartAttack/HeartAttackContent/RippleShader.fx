float4x4 World;
float4x4 View;
float4x4 Projection;

float2 Viewport;
float4 pingLengths;


uniform extern texture ScreenTexture;    

sampler ScreenS = sampler_state
{
	Texture = <ScreenTexture>;    
};

// TODO: add effect parameters here.

struct VertexShaderInput
{
	float4 Position : POSITION0;
	float2 texCoord : TEXCOORD;

	// TODO: add input channels such as texture
	// coordinates and vertex colors here.
};

struct VertexShaderOutput
{
	float4 Position : POSITION0;
	float2 texCoord : TEXCOORD;
	// TODO: add vertex shader outputs such as colors and texture
	// coordinates here. These values will automatically be interpolated
	// over the triangle, and provided as input to your pixel shader.
};

VertexShaderOutput VertexShaderFunction(VertexShaderInput input)
{
	VertexShaderOutput output;

	float4 worldPosition = mul(input.Position, World);
	float4 viewPosition = mul(worldPosition, View);
	output.Position = mul(viewPosition, Projection);
	output.texCoord = input.texCoord;
	// TODO: add your vertex shader code here.

	return output;
}

float4 PixelShaderFunction(VertexShaderOutput input) : COLOR0
{	
	// change to pixels from centre
	float2 pixelCoord = input.texCoord;
	pixelCoord.x *= Viewport.x;
	pixelCoord.y *= Viewport.y;
	pixelCoord.x -= Viewport.x/2;
	pixelCoord.y -= Viewport.y/2;
	float dist = length(pixelCoord);

	float threshold = 35;

	float offset = 0;
	if(dist - pingLengths.x < threshold &&
		dist - pingLengths.x > -threshold)
		{
			offset = abs((abs(dist - pingLengths.x))/threshold - 1);
		}
	else if(dist - pingLengths.y < threshold &&
		dist - pingLengths.y > -threshold)
		{
			offset = abs((abs(dist - pingLengths.y))/threshold - 1);
		}
	else if(dist - pingLengths.z < threshold &&
		dist - pingLengths.z > -threshold)
		{
			offset = abs((abs(dist - pingLengths.z))/threshold - 1);
		}
	else if(dist - pingLengths.w < threshold &&
		dist - pingLengths.w > -threshold)
		{
			offset = abs((abs(dist - pingLengths.w))/threshold - 1);
		}

		float2 offsetVector = input.texCoord - 0.5;

		float mag = 0.05;

		offsetVector *= offset * mag;
		// pick a pixel offset away in the direction
		//float scalar = length(distance);
//
		//// invert the scale so 1 is centerpoint
		//scalar = abs(1 - scalar);
		//float wave = 8;
		//float distortion = 10;
		//// calculate how far to distort for this pixel    
		//float sinoffset = sin(wave / scalar);
		//sinoffset = clamp(sinoffset, 0, 1);
    //
		//// calculate which direction to distort
		//float sinsign = cos(wave / scalar);    
    //
		//// reduce the distortion effect
		//sinoffset = sinoffset * distortion/32;	

	
	return tex2D(ScreenS, input.texCoord + offsetVector); 

}

technique Technique1
{
	pass Pass1
	{
		// TODO: set renderstates here.

		//VertexShader = compile vs_2_0 VertexShaderFunction();
		PixelShader = compile ps_2_0 PixelShaderFunction();
	}
}
