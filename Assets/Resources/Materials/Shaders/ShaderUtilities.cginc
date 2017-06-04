float mod(float x, float y) {
	return floor(frac(x / y) * y);
}

float div(float x, float y) {
	return floor(x / y);
}

float3 rgbToHsl(float3 color) {
	float maximum = max(color.r, max(color.g, color.b));
	float minimum = min(color.r, min(color.g, color.b));
	float h = (maximum + minimum) / 2;
	float s = h; float l = h;

	if (maximum == minimum) {
		h = 0;
		s = 0;
	}
	else {
		float d = maximum - minimum;
		s = l > 0.5 ? d / (2.0 - maximum - minimum) : d / (maximum + minimum);
		if (color.r == maximum) {
			h = (color.g - color.b) / d + (color.g < color.b ? 6 : 0);
		}
		else if (color.g == maximum) {
			h = (color.b - color.r) / d + 2;
		}
		else if (color.b == maximum) {
			h = (color.r - color.g) / d + 4;
		}
		h /= 6.0;
	}

	return float3(h,s,l);
}

float hue2rgb(float p, float q, float t) {
	if (t < 0) t += 1;
	if (t > 1) t -= 1;
	if (t < 1 / 6) return p + (q - p) * 6 * t;
	if (t < 1 / 2) return q;
	if (t < 2 / 3) return p + (q - p) * (2.0 / 3.0 - t) * 6;
	return p;
}

float3 hslToRgb(float3 color) {
	float h = color.x; 
	float s = color.y; 
	float l = color.z;
	float r;
	float g;
	float b;
	if (s == 0) {
		r = l;
		g = l;
		b = l;
	}
	else {		

		float q = l < 0.5 ? l * (1 + s) : l + s - l * s;
		float p = 2 * l - q;
		r = hue2rgb(p, q, h + 1.0 / 3.0);
		g = hue2rgb(p, q, h);
		b = hue2rgb(p, q, h - 1.0 / 3.0);
	}

	return float3(r, g, b);
}

float3 HUEtoRGB(in float H)
{
	float R = abs(H * 6 - 3) - 1;
	float G = 2 - abs(H * 6 - 2);
	float B = 2 - abs(H * 6 - 4);
	return saturate(float3(R, G, B));
}
float3 HSLtoRGB(in float3 HSL)
{
	float3 RGB = HUEtoRGB(HSL.x);
	float C = (1 - abs(2 * HSL.z - 1)) * HSL.y;
	return (RGB - 0.5) * C + HSL.z;
}
float Epsilon = 1e-10;
float3 RGBtoHCV(in float3 RGB)
{
	// Based on work by Sam Hocevar and Emil Persson
	float4 P = (RGB.g < RGB.b) ? float4(RGB.bg, -1.0, 2.0 / 3.0) : float4(RGB.gb, 0.0, -1.0 / 3.0);
	float4 Q = (RGB.r < P.x) ? float4(P.xyw, RGB.r) : float4(RGB.r, P.yzx);
	float C = Q.x - min(Q.w, Q.y);
	float H = abs((Q.w - Q.y) / (6 * C + Epsilon) + Q.z);
	return float3(H, C, Q.x);
}

float3 RGBtoHSL(in float3 RGB)
{
	float3 HCV = RGBtoHCV(RGB);
	float L = HCV.z - HCV.y * 0.5;
	float S = HCV.y / (1 - abs(L * 2 - 1) + Epsilon);
	return float3(HCV.x, S, L);
}