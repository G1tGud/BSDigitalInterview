﻿using System.Text.Json;
using CryptoExchangeApi.Data;
using CryptoExchangeApi.Models;
using CryptoExchangeApi.Services;
using Microsoft.Extensions.Logging.Abstractions;
using Moq;

namespace CryptoExchangeTest;

public class MetaExchangeServiceTest
{
    private readonly BookOrder _bookOrders1 = JsonSerializer.Deserialize<BookOrder>("""
       {"AcqTime":"2019-01-29T11:50:46.9453546Z","Bids":[{"Order":{"Id":null,"Time":"0001-01-01T00:00:00","Type":"Buy","Kind":"Limit","Amount":13.10126816,"Price":2965.0}},{"Order":{"Id":null,"Time":"0001-01-01T00:00:00","Type":"Buy","Kind":"Limit","Amount":0.10084002,"Price":2963.01}},{"Order":{"Id":null,"Time":"0001-01-01T00:00:00","Type":"Buy","Kind":"Limit","Amount":0.033,"Price":2960.0}},{"Order":{"Id":null,"Time":"0001-01-01T00:00:00","Type":"Buy","Kind":"Limit","Amount":4.48834308,"Price":2957.59}},{"Order":{"Id":null,"Time":"0001-01-01T00:00:00","Type":"Buy","Kind":"Limit","Amount":2.0,"Price":2957.58}},{"Order":{"Id":null,"Time":"0001-01-01T00:00:00","Type":"Buy","Kind":"Limit","Amount":0.01226619,"Price":2957.02}},{"Order":{"Id":null,"Time":"0001-01-01T00:00:00","Type":"Buy","Kind":"Limit","Amount":0.00473008,"Price":2956.53}},{"Order":{"Id":null,"Time":"0001-01-01T00:00:00","Type":"Buy","Kind":"Limit","Amount":0.1,"Price":2955.0}},{"Order":{"Id":null,"Time":"0001-01-01T00:00:00","Type":"Buy","Kind":"Limit","Amount":0.01,"Price":2954.43}},{"Order":{"Id":null,"Time":"0001-01-01T00:00:00","Type":"Buy","Kind":"Limit","Amount":0.431,"Price":2954.1}},{"Order":{"Id":null,"Time":"0001-01-01T00:00:00","Type":"Buy","Kind":"Limit","Amount":13.0,"Price":2953.12}},{"Order":{"Id":null,"Time":"0001-01-01T00:00:00","Type":"Buy","Kind":"Limit","Amount":1.0,"Price":2953.0}},{"Order":{"Id":null,"Time":"0001-01-01T00:00:00","Type":"Buy","Kind":"Limit","Amount":0.431,"Price":2952.62}},{"Order":{"Id":null,"Time":"0001-01-01T00:00:00","Type":"Buy","Kind":"Limit","Amount":1.25,"Price":2952.3}},{"Order":{"Id":null,"Time":"0001-01-01T00:00:00","Type":"Buy","Kind":"Limit","Amount":0.49,"Price":2951.98}},{"Order":{"Id":null,"Time":"0001-01-01T00:00:00","Type":"Buy","Kind":"Limit","Amount":0.0808587,"Price":2951.71}},{"Order":{"Id":null,"Time":"0001-01-01T00:00:00","Type":"Buy","Kind":"Limit","Amount":2.312,"Price":2951.69}},{"Order":{"Id":null,"Time":"0001-01-01T00:00:00","Type":"Buy","Kind":"Limit","Amount":0.02423993,"Price":2951.35}},{"Order":{"Id":null,"Time":"0001-01-01T00:00:00","Type":"Buy","Kind":"Limit","Amount":0.12793338,"Price":2950.61}},{"Order":{"Id":null,"Time":"0001-01-01T00:00:00","Type":"Buy","Kind":"Limit","Amount":24.53842278,"Price":2950.58}},{"Order":{"Id":null,"Time":"0001-01-01T00:00:00","Type":"Buy","Kind":"Limit","Amount":3.3814,"Price":2950.0}},{"Order":{"Id":null,"Time":"0001-01-01T00:00:00","Type":"Buy","Kind":"Limit","Amount":0.431,"Price":2949.45}},{"Order":{"Id":null,"Time":"0001-01-01T00:00:00","Type":"Buy","Kind":"Limit","Amount":2.376,"Price":2949.43}},{"Order":{"Id":null,"Time":"0001-01-01T00:00:00","Type":"Buy","Kind":"Limit","Amount":2.53128999,"Price":2949.4}},{"Order":{"Id":null,"Time":"0001-01-01T00:00:00","Type":"Buy","Kind":"Limit","Amount":0.4,"Price":2947.93}},{"Order":{"Id":null,"Time":"0001-01-01T00:00:00","Type":"Buy","Kind":"Limit","Amount":0.43,"Price":2947.41}},{"Order":{"Id":null,"Time":"0001-01-01T00:00:00","Type":"Buy","Kind":"Limit","Amount":1.521,"Price":2947.07}},{"Order":{"Id":null,"Time":"0001-01-01T00:00:00","Type":"Buy","Kind":"Limit","Amount":0.2,"Price":2946.73}},{"Order":{"Id":null,"Time":"0001-01-01T00:00:00","Type":"Buy","Kind":"Limit","Amount":0.431,"Price":2945.72}},{"Order":{"Id":null,"Time":"0001-01-01T00:00:00","Type":"Buy","Kind":"Limit","Amount":1.75,"Price":2945.3}},{"Order":{"Id":null,"Time":"0001-01-01T00:00:00","Type":"Buy","Kind":"Limit","Amount":1.219,"Price":2945.01}},{"Order":{"Id":null,"Time":"0001-01-01T00:00:00","Type":"Buy","Kind":"Limit","Amount":0.431,"Price":2944.03}},{"Order":{"Id":null,"Time":"0001-01-01T00:00:00","Type":"Buy","Kind":"Limit","Amount":26.0,"Price":2943.16}},{"Order":{"Id":null,"Time":"0001-01-01T00:00:00","Type":"Buy","Kind":"Limit","Amount":8.05,"Price":2943.15}},{"Order":{"Id":null,"Time":"0001-01-01T00:00:00","Type":"Buy","Kind":"Limit","Amount":0.431,"Price":2942.34}},{"Order":{"Id":null,"Time":"0001-01-01T00:00:00","Type":"Buy","Kind":"Limit","Amount":0.84664724,"Price":2941.48}},{"Order":{"Id":null,"Time":"0001-01-01T00:00:00","Type":"Buy","Kind":"Limit","Amount":25.3129,"Price":2941.46}},{"Order":{"Id":null,"Time":"0001-01-01T00:00:00","Type":"Buy","Kind":"Limit","Amount":0.8,"Price":2941.0}},{"Order":{"Id":null,"Time":"0001-01-01T00:00:00","Type":"Buy","Kind":"Limit","Amount":0.02423993,"Price":2940.29}},{"Order":{"Id":null,"Time":"0001-01-01T00:00:00","Type":"Buy","Kind":"Limit","Amount":9.0,"Price":2940.28}},{"Order":{"Id":null,"Time":"0001-01-01T00:00:00","Type":"Buy","Kind":"Limit","Amount":0.031,"Price":2940.0}},{"Order":{"Id":null,"Time":"0001-01-01T00:00:00","Type":"Buy","Kind":"Limit","Amount":0.13336696,"Price":2939.92}},{"Order":{"Id":null,"Time":"0001-01-01T00:00:00","Type":"Buy","Kind":"Limit","Amount":0.31,"Price":2939.45}},{"Order":{"Id":null,"Time":"0001-01-01T00:00:00","Type":"Buy","Kind":"Limit","Amount":7.0,"Price":2937.41}},{"Order":{"Id":null,"Time":"0001-01-01T00:00:00","Type":"Buy","Kind":"Limit","Amount":0.31,"Price":2936.49}},{"Order":{"Id":null,"Time":"0001-01-01T00:00:00","Type":"Buy","Kind":"Limit","Amount":5.36,"Price":2934.38}},{"Order":{"Id":null,"Time":"0001-01-01T00:00:00","Type":"Buy","Kind":"Limit","Amount":0.31,"Price":2933.54}},{"Order":{"Id":null,"Time":"0001-01-01T00:00:00","Type":"Buy","Kind":"Limit","Amount":0.1058635,"Price":2932.35}},{"Order":{"Id":null,"Time":"0001-01-01T00:00:00","Type":"Buy","Kind":"Limit","Amount":5.27725981,"Price":2931.64}},{"Order":{"Id":null,"Time":"0001-01-01T00:00:00","Type":"Buy","Kind":"Limit","Amount":2.0,"Price":2931.2}},{"Order":{"Id":null,"Time":"0001-01-01T00:00:00","Type":"Buy","Kind":"Limit","Amount":8.12,"Price":2931.18}},{"Order":{"Id":null,"Time":"0001-01-01T00:00:00","Type":"Buy","Kind":"Limit","Amount":0.07,"Price":2930.58}},{"Order":{"Id":null,"Time":"0001-01-01T00:00:00","Type":"Buy","Kind":"Limit","Amount":1.19799956,"Price":2929.9}},{"Order":{"Id":null,"Time":"0001-01-01T00:00:00","Type":"Buy","Kind":"Limit","Amount":0.02423993,"Price":2929.23}},{"Order":{"Id":null,"Time":"0001-01-01T00:00:00","Type":"Buy","Kind":"Limit","Amount":0.23,"Price":2929.0}},{"Order":{"Id":null,"Time":"0001-01-01T00:00:00","Type":"Buy","Kind":"Limit","Amount":11.7,"Price":2927.9}},{"Order":{"Id":null,"Time":"0001-01-01T00:00:00","Type":"Buy","Kind":"Limit","Amount":0.3,"Price":2925.9}},{"Order":{"Id":null,"Time":"0001-01-01T00:00:00","Type":"Buy","Kind":"Limit","Amount":0.39386842,"Price":2925.44}},{"Order":{"Id":null,"Time":"0001-01-01T00:00:00","Type":"Buy","Kind":"Limit","Amount":0.2185,"Price":2925.0}},{"Order":{"Id":null,"Time":"0001-01-01T00:00:00","Type":"Buy","Kind":"Limit","Amount":12.5,"Price":2924.83}},{"Order":{"Id":null,"Time":"0001-01-01T00:00:00","Type":"Buy","Kind":"Limit","Amount":0.00654099,"Price":2924.6}},{"Order":{"Id":null,"Time":"0001-01-01T00:00:00","Type":"Buy","Kind":"Limit","Amount":0.5,"Price":2923.88}},{"Order":{"Id":null,"Time":"0001-01-01T00:00:00","Type":"Buy","Kind":"Limit","Amount":33.28,"Price":2922.7}},{"Order":{"Id":null,"Time":"0001-01-01T00:00:00","Type":"Buy","Kind":"Limit","Amount":0.13608699,"Price":2922.42}},{"Order":{"Id":null,"Time":"0001-01-01T00:00:00","Type":"Buy","Kind":"Limit","Amount":0.003265,"Price":2922.21}},{"Order":{"Id":null,"Time":"0001-01-01T00:00:00","Type":"Buy","Kind":"Limit","Amount":6.6,"Price":2921.65}},{"Order":{"Id":null,"Time":"0001-01-01T00:00:00","Type":"Buy","Kind":"Limit","Amount":0.2,"Price":2920.57}},{"Order":{"Id":null,"Time":"0001-01-01T00:00:00","Type":"Buy","Kind":"Limit","Amount":0.06832089,"Price":2920.19}},{"Order":{"Id":null,"Time":"0001-01-01T00:00:00","Type":"Buy","Kind":"Limit","Amount":0.1140017,"Price":2920.0}},{"Order":{"Id":null,"Time":"0001-01-01T00:00:00","Type":"Buy","Kind":"Limit","Amount":5.05,"Price":2919.04}},{"Order":{"Id":null,"Time":"0001-01-01T00:00:00","Type":"Buy","Kind":"Limit","Amount":0.01,"Price":2919.0}},{"Order":{"Id":null,"Time":"0001-01-01T00:00:00","Type":"Buy","Kind":"Limit","Amount":0.12874705,"Price":2918.28}},{"Order":{"Id":null,"Time":"0001-01-01T00:00:00","Type":"Buy","Kind":"Limit","Amount":6.397,"Price":2918.25}},{"Order":{"Id":null,"Time":"0001-01-01T00:00:00","Type":"Buy","Kind":"Limit","Amount":0.56944139,"Price":2918.0}},{"Order":{"Id":null,"Time":"0001-01-01T00:00:00","Type":"Buy","Kind":"Limit","Amount":0.02423994,"Price":2917.32}},{"Order":{"Id":null,"Time":"0001-01-01T00:00:00","Type":"Buy","Kind":"Limit","Amount":5.24,"Price":2916.08}},{"Order":{"Id":null,"Time":"0001-01-01T00:00:00","Type":"Buy","Kind":"Limit","Amount":0.047249,"Price":2915.0}},{"Order":{"Id":null,"Time":"0001-01-01T00:00:00","Type":"Buy","Kind":"Limit","Amount":0.00459,"Price":2914.25}},{"Order":{"Id":null,"Time":"0001-01-01T00:00:00","Type":"Buy","Kind":"Limit","Amount":11.735,"Price":2913.78}},{"Order":{"Id":null,"Time":"0001-01-01T00:00:00","Type":"Buy","Kind":"Limit","Amount":10.1,"Price":2913.6}},{"Order":{"Id":null,"Time":"0001-01-01T00:00:00","Type":"Buy","Kind":"Limit","Amount":0.03424,"Price":2913.03}},{"Order":{"Id":null,"Time":"0001-01-01T00:00:00","Type":"Buy","Kind":"Limit","Amount":32.8,"Price":2912.96}},{"Order":{"Id":null,"Time":"0001-01-01T00:00:00","Type":"Buy","Kind":"Limit","Amount":0.03425363,"Price":2912.1}},{"Order":{"Id":null,"Time":"0001-01-01T00:00:00","Type":"Buy","Kind":"Limit","Amount":0.3558,"Price":2912.0}},{"Order":{"Id":null,"Time":"0001-01-01T00:00:00","Type":"Buy","Kind":"Limit","Amount":0.32608292,"Price":2911.0}},{"Order":{"Id":null,"Time":"0001-01-01T00:00:00","Type":"Buy","Kind":"Limit","Amount":7.94,"Price":2910.87}},{"Order":{"Id":null,"Time":"0001-01-01T00:00:00","Type":"Buy","Kind":"Limit","Amount":0.5,"Price":2910.51}},{"Order":{"Id":null,"Time":"0001-01-01T00:00:00","Type":"Buy","Kind":"Limit","Amount":0.52094196,"Price":2910.0}},{"Order":{"Id":null,"Time":"0001-01-01T00:00:00","Type":"Buy","Kind":"Limit","Amount":12.65645,"Price":2909.01}},{"Order":{"Id":null,"Time":"0001-01-01T00:00:00","Type":"Buy","Kind":"Limit","Amount":0.01152942,"Price":2907.24}},{"Order":{"Id":null,"Time":"0001-01-01T00:00:00","Type":"Buy","Kind":"Limit","Amount":0.02,"Price":2907.0}},{"Order":{"Id":null,"Time":"0001-01-01T00:00:00","Type":"Buy","Kind":"Limit","Amount":0.004,"Price":2906.73}},{"Order":{"Id":null,"Time":"0001-01-01T00:00:00","Type":"Buy","Kind":"Limit","Amount":0.51211105,"Price":2906.0}},{"Order":{"Id":null,"Time":"0001-01-01T00:00:00","Type":"Buy","Kind":"Limit","Amount":0.05,"Price":2905.72}},{"Order":{"Id":null,"Time":"0001-01-01T00:00:00","Type":"Buy","Kind":"Limit","Amount":0.68812,"Price":2905.0}},{"Order":{"Id":null,"Time":"0001-01-01T00:00:00","Type":"Buy","Kind":"Limit","Amount":9.542,"Price":2904.86}},{"Order":{"Id":null,"Time":"0001-01-01T00:00:00","Type":"Buy","Kind":"Limit","Amount":0.15,"Price":2903.0}},{"Order":{"Id":null,"Time":"0001-01-01T00:00:00","Type":"Buy","Kind":"Limit","Amount":0.0275,"Price":2902.2}},{"Order":{"Id":null,"Time":"0001-01-01T00:00:00","Type":"Buy","Kind":"Limit","Amount":10.04,"Price":2902.0}},{"Order":{"Id":null,"Time":"0001-01-01T00:00:00","Type":"Buy","Kind":"Limit","Amount":0.1,"Price":2901.95}}],"Asks":[{"Order":{"Id":null,"Time":"0001-01-01T00:00:00","Type":"Sell","Kind":"Limit","Amount":5.37758595,"Price":2965.01}},{"Order":{"Id":null,"Time":"0001-01-01T00:00:00","Type":"Sell","Kind":"Limit","Amount":0.405,"Price":2965.02}},{"Order":{"Id":null,"Time":"0001-01-01T00:00:00","Type":"Sell","Kind":"Limit","Amount":0.2,"Price":2966.05}},{"Order":{"Id":null,"Time":"0001-01-01T00:00:00","Type":"Sell","Kind":"Limit","Amount":1.955,"Price":2967.15}},{"Order":{"Id":null,"Time":"0001-01-01T00:00:00","Type":"Sell","Kind":"Limit","Amount":0.09518,"Price":2967.43}},{"Order":{"Id":null,"Time":"0001-01-01T00:00:00","Type":"Sell","Kind":"Limit","Amount":0.405,"Price":2967.44}},{"Order":{"Id":null,"Time":"0001-01-01T00:00:00","Type":"Sell","Kind":"Limit","Amount":1.6,"Price":2968.54}},{"Order":{"Id":null,"Time":"0001-01-01T00:00:00","Type":"Sell","Kind":"Limit","Amount":0.5,"Price":2968.56}},{"Order":{"Id":null,"Time":"0001-01-01T00:00:00","Type":"Sell","Kind":"Limit","Amount":0.405,"Price":2969.16}},{"Order":{"Id":null,"Time":"0001-01-01T00:00:00","Type":"Sell","Kind":"Limit","Amount":2.53128999,"Price":2969.58}},{"Order":{"Id":null,"Time":"0001-01-01T00:00:00","Type":"Sell","Kind":"Limit","Amount":0.118,"Price":2970.0}},{"Order":{"Id":null,"Time":"0001-01-01T00:00:00","Type":"Sell","Kind":"Limit","Amount":0.404,"Price":2970.86}},{"Order":{"Id":null,"Time":"0001-01-01T00:00:00","Type":"Sell","Kind":"Limit","Amount":0.12634646,"Price":2971.78}},{"Order":{"Id":null,"Time":"0001-01-01T00:00:00","Type":"Sell","Kind":"Limit","Amount":6.3983869,"Price":2972.23}},{"Order":{"Id":null,"Time":"0001-01-01T00:00:00","Type":"Sell","Kind":"Limit","Amount":2.0,"Price":2972.44}},{"Order":{"Id":null,"Time":"0001-01-01T00:00:00","Type":"Sell","Kind":"Limit","Amount":0.404,"Price":2972.56}},{"Order":{"Id":null,"Time":"0001-01-01T00:00:00","Type":"Sell","Kind":"Limit","Amount":17.775,"Price":2972.64}},{"Order":{"Id":null,"Time":"0001-01-01T00:00:00","Type":"Sell","Kind":"Limit","Amount":1.18,"Price":2973.03}},{"Order":{"Id":null,"Time":"0001-01-01T00:00:00","Type":"Sell","Kind":"Limit","Amount":10.0,"Price":2973.19}},{"Order":{"Id":null,"Time":"0001-01-01T00:00:00","Type":"Sell","Kind":"Limit","Amount":0.404,"Price":2974.25}},{"Order":{"Id":null,"Time":"0001-01-01T00:00:00","Type":"Sell","Kind":"Limit","Amount":0.02388,"Price":2974.99}},{"Order":{"Id":null,"Time":"0001-01-01T00:00:00","Type":"Sell","Kind":"Limit","Amount":2.0,"Price":2975.0}},{"Order":{"Id":null,"Time":"0001-01-01T00:00:00","Type":"Sell","Kind":"Limit","Amount":3.59907016,"Price":2976.46}},{"Order":{"Id":null,"Time":"0001-01-01T00:00:00","Type":"Sell","Kind":"Limit","Amount":1.135,"Price":2976.48}},{"Order":{"Id":null,"Time":"0001-01-01T00:00:00","Type":"Sell","Kind":"Limit","Amount":0.12634646,"Price":2978.12}},{"Order":{"Id":null,"Time":"0001-01-01T00:00:00","Type":"Sell","Kind":"Limit","Amount":25.3129,"Price":2978.48}},{"Order":{"Id":null,"Time":"0001-01-01T00:00:00","Type":"Sell","Kind":"Limit","Amount":2.376,"Price":2979.07}},{"Order":{"Id":null,"Time":"0001-01-01T00:00:00","Type":"Sell","Kind":"Limit","Amount":0.02387,"Price":2984.14}},{"Order":{"Id":null,"Time":"0001-01-01T00:00:00","Type":"Sell","Kind":"Limit","Amount":0.308,"Price":2984.83}},{"Order":{"Id":null,"Time":"0001-01-01T00:00:00","Type":"Sell","Kind":"Limit","Amount":0.573,"Price":2984.85}},{"Order":{"Id":null,"Time":"0001-01-01T00:00:00","Type":"Sell","Kind":"Limit","Amount":2.0,"Price":2985.0}},{"Order":{"Id":null,"Time":"0001-01-01T00:00:00","Type":"Sell","Kind":"Limit","Amount":0.12634646,"Price":2985.04}},{"Order":{"Id":null,"Time":"0001-01-01T00:00:00","Type":"Sell","Kind":"Limit","Amount":1.521,"Price":2986.54}},{"Order":{"Id":null,"Time":"0001-01-01T00:00:00","Type":"Sell","Kind":"Limit","Amount":0.5,"Price":2988.11}},{"Order":{"Id":null,"Time":"0001-01-01T00:00:00","Type":"Sell","Kind":"Limit","Amount":2.212,"Price":2989.92}},{"Order":{"Id":null,"Time":"0001-01-01T00:00:00","Type":"Sell","Kind":"Limit","Amount":0.12634646,"Price":2991.38}},{"Order":{"Id":null,"Time":"0001-01-01T00:00:00","Type":"Sell","Kind":"Limit","Amount":0.54945725,"Price":2994.55}},{"Order":{"Id":null,"Time":"0001-01-01T00:00:00","Type":"Sell","Kind":"Limit","Amount":0.2,"Price":2994.56}},{"Order":{"Id":null,"Time":"0001-01-01T00:00:00","Type":"Sell","Kind":"Limit","Amount":2.0,"Price":2995.0}},{"Order":{"Id":null,"Time":"0001-01-01T00:00:00","Type":"Sell","Kind":"Limit","Amount":0.53588928,"Price":2995.03}},{"Order":{"Id":null,"Time":"0001-01-01T00:00:00","Type":"Sell","Kind":"Limit","Amount":0.033,"Price":2995.35}},{"Order":{"Id":null,"Time":"0001-01-01T00:00:00","Type":"Sell","Kind":"Limit","Amount":0.31,"Price":2995.74}},{"Order":{"Id":null,"Time":"0001-01-01T00:00:00","Type":"Sell","Kind":"Limit","Amount":0.00646311,"Price":2995.81}},{"Order":{"Id":null,"Time":"0001-01-01T00:00:00","Type":"Sell","Kind":"Limit","Amount":0.1,"Price":2998.0}},{"Order":{"Id":null,"Time":"0001-01-01T00:00:00","Type":"Sell","Kind":"Limit","Amount":0.00486034,"Price":2998.26}},{"Order":{"Id":null,"Time":"0001-01-01T00:00:00","Type":"Sell","Kind":"Limit","Amount":0.31,"Price":2998.7}},{"Order":{"Id":null,"Time":"0001-01-01T00:00:00","Type":"Sell","Kind":"Limit","Amount":13.7,"Price":3000.0}},{"Order":{"Id":null,"Time":"0001-01-01T00:00:00","Type":"Sell","Kind":"Limit","Amount":0.31,"Price":3001.65}},{"Order":{"Id":null,"Time":"0001-01-01T00:00:00","Type":"Sell","Kind":"Limit","Amount":4.495,"Price":3003.31}},{"Order":{"Id":null,"Time":"0001-01-01T00:00:00","Type":"Sell","Kind":"Limit","Amount":0.05,"Price":3003.7}},{"Order":{"Id":null,"Time":"0001-01-01T00:00:00","Type":"Sell","Kind":"Limit","Amount":0.07,"Price":3004.61}},{"Order":{"Id":null,"Time":"0001-01-01T00:00:00","Type":"Sell","Kind":"Limit","Amount":0.25,"Price":3005.0}},{"Order":{"Id":null,"Time":"0001-01-01T00:00:00","Type":"Sell","Kind":"Limit","Amount":0.25,"Price":3006.0}},{"Order":{"Id":null,"Time":"0001-01-01T00:00:00","Type":"Sell","Kind":"Limit","Amount":0.25,"Price":3007.0}},{"Order":{"Id":null,"Time":"0001-01-01T00:00:00","Type":"Sell","Kind":"Limit","Amount":0.31,"Price":3007.57}},{"Order":{"Id":null,"Time":"0001-01-01T00:00:00","Type":"Sell","Kind":"Limit","Amount":18.87308352,"Price":3009.99}},{"Order":{"Id":null,"Time":"0001-01-01T00:00:00","Type":"Sell","Kind":"Limit","Amount":0.1,"Price":3010.0}},{"Order":{"Id":null,"Time":"0001-01-01T00:00:00","Type":"Sell","Kind":"Limit","Amount":4.07375647,"Price":3011.79}},{"Order":{"Id":null,"Time":"0001-01-01T00:00:00","Type":"Sell","Kind":"Limit","Amount":0.005123,"Price":3015.84}},{"Order":{"Id":null,"Time":"0001-01-01T00:00:00","Type":"Sell","Kind":"Limit","Amount":10.595,"Price":3016.7}},{"Order":{"Id":null,"Time":"0001-01-01T00:00:00","Type":"Sell","Kind":"Limit","Amount":0.00382481,"Price":3016.77}},{"Order":{"Id":null,"Time":"0001-01-01T00:00:00","Type":"Sell","Kind":"Limit","Amount":0.5,"Price":3017.0}},{"Order":{"Id":null,"Time":"0001-01-01T00:00:00","Type":"Sell","Kind":"Limit","Amount":0.2,"Price":3020.0}},{"Order":{"Id":null,"Time":"0001-01-01T00:00:00","Type":"Sell","Kind":"Limit","Amount":0.0047,"Price":3020.3}},{"Order":{"Id":null,"Time":"0001-01-01T00:00:00","Type":"Sell","Kind":"Limit","Amount":0.01609676,"Price":3023.59}},{"Order":{"Id":null,"Time":"0001-01-01T00:00:00","Type":"Sell","Kind":"Limit","Amount":2.002,"Price":3025.0}},{"Order":{"Id":null,"Time":"0001-01-01T00:00:00","Type":"Sell","Kind":"Limit","Amount":0.3,"Price":3030.0}},{"Order":{"Id":null,"Time":"0001-01-01T00:00:00","Type":"Sell","Kind":"Limit","Amount":7.897,"Price":3030.09}},{"Order":{"Id":null,"Time":"0001-01-01T00:00:00","Type":"Sell","Kind":"Limit","Amount":0.4,"Price":3033.35}},{"Order":{"Id":null,"Time":"0001-01-01T00:00:00","Type":"Sell","Kind":"Limit","Amount":0.00743778,"Price":3038.12}},{"Order":{"Id":null,"Time":"0001-01-01T00:00:00","Type":"Sell","Kind":"Limit","Amount":0.4,"Price":3040.0}},{"Order":{"Id":null,"Time":"0001-01-01T00:00:00","Type":"Sell","Kind":"Limit","Amount":0.0215792,"Price":3040.88}},{"Order":{"Id":null,"Time":"0001-01-01T00:00:00","Type":"Sell","Kind":"Limit","Amount":0.1,"Price":3043.38}},{"Order":{"Id":null,"Time":"0001-01-01T00:00:00","Type":"Sell","Kind":"Limit","Amount":0.02042978,"Price":3049.99}},{"Order":{"Id":null,"Time":"0001-01-01T00:00:00","Type":"Sell","Kind":"Limit","Amount":1.510492,"Price":3050.0}},{"Order":{"Id":null,"Time":"0001-01-01T00:00:00","Type":"Sell","Kind":"Limit","Amount":1.0,"Price":3051.0}},{"Order":{"Id":null,"Time":"0001-01-01T00:00:00","Type":"Sell","Kind":"Limit","Amount":0.04,"Price":3054.1}},{"Order":{"Id":null,"Time":"0001-01-01T00:00:00","Type":"Sell","Kind":"Limit","Amount":0.6,"Price":3060.0}},{"Order":{"Id":null,"Time":"0001-01-01T00:00:00","Type":"Sell","Kind":"Limit","Amount":0.044,"Price":3060.37}},{"Order":{"Id":null,"Time":"0001-01-01T00:00:00","Type":"Sell","Kind":"Limit","Amount":0.0048951,"Price":3064.29}},{"Order":{"Id":null,"Time":"0001-01-01T00:00:00","Type":"Sell","Kind":"Limit","Amount":0.7449,"Price":3070.0}},{"Order":{"Id":null,"Time":"0001-01-01T00:00:00","Type":"Sell","Kind":"Limit","Amount":0.0213695,"Price":3070.71}},{"Order":{"Id":null,"Time":"0001-01-01T00:00:00","Type":"Sell","Kind":"Limit","Amount":0.03187699,"Price":3074.0}},{"Order":{"Id":null,"Time":"0001-01-01T00:00:00","Type":"Sell","Kind":"Limit","Amount":0.03322641,"Price":3075.0}},{"Order":{"Id":null,"Time":"0001-01-01T00:00:00","Type":"Sell","Kind":"Limit","Amount":3.1,"Price":3080.0}},{"Order":{"Id":null,"Time":"0001-01-01T00:00:00","Type":"Sell","Kind":"Limit","Amount":0.00837082,"Price":3087.19}},{"Order":{"Id":null,"Time":"0001-01-01T00:00:00","Type":"Sell","Kind":"Limit","Amount":2.00817225,"Price":3088.9}},{"Order":{"Id":null,"Time":"0001-01-01T00:00:00","Type":"Sell","Kind":"Limit","Amount":0.0017,"Price":3089.0}},{"Order":{"Id":null,"Time":"0001-01-01T00:00:00","Type":"Sell","Kind":"Limit","Amount":0.2339,"Price":3090.01}},{"Order":{"Id":null,"Time":"0001-01-01T00:00:00","Type":"Sell","Kind":"Limit","Amount":0.003729,"Price":3094.12}},{"Order":{"Id":null,"Time":"0001-01-01T00:00:00","Type":"Sell","Kind":"Limit","Amount":0.08,"Price":3099.0}},{"Order":{"Id":null,"Time":"0001-01-01T00:00:00","Type":"Sell","Kind":"Limit","Amount":0.0245,"Price":3099.99}},{"Order":{"Id":null,"Time":"0001-01-01T00:00:00","Type":"Sell","Kind":"Limit","Amount":10.45090666,"Price":3100.0}},{"Order":{"Id":null,"Time":"0001-01-01T00:00:00","Type":"Sell","Kind":"Limit","Amount":0.0018655,"Price":3100.77}},{"Order":{"Id":null,"Time":"0001-01-01T00:00:00","Type":"Sell","Kind":"Limit","Amount":0.004975,"Price":3105.45}},{"Order":{"Id":null,"Time":"0001-01-01T00:00:00","Type":"Sell","Kind":"Limit","Amount":1.0,"Price":3106.0}},{"Order":{"Id":null,"Time":"0001-01-01T00:00:00","Type":"Sell","Kind":"Limit","Amount":0.01729,"Price":3107.0}},{"Order":{"Id":null,"Time":"0001-01-01T00:00:00","Type":"Sell","Kind":"Limit","Amount":0.07875741,"Price":3107.51}},{"Order":{"Id":null,"Time":"0001-01-01T00:00:00","Type":"Sell","Kind":"Limit","Amount":20.0,"Price":3109.99}},{"Order":{"Id":null,"Time":"0001-01-01T00:00:00","Type":"Sell","Kind":"Limit","Amount":0.04726449,"Price":3111.51}}]}
    """);

    private static readonly Order Order1 = new Order
    {
        Amount = 4,
        Price = 5
    };
    
    private static readonly Order Order2 = new Order
    {
        Amount = 3,
        Price = 7
    };
    
    private static readonly Order Order3 = new Order
    {
        Amount = 1,
        Price = 2
    };
    
    
    private IMetaExchangeService _sut = new MetaExchangeService(new CryptoExchangeDataProvider(new CryptoExchangeRepository()), new NullLoggerFactory());

    [Theory]
    [InlineData(0)]
    [InlineData(-1.2)]
    public void ShouldThrowExceptionIfAmountIsNonPositive(decimal amount)
    {
        //setup
        var request = new ExecutionRequest()
        {
            Amount = amount
        };
        
        // Act & Assert
        var exception = Assert.Throws<ArgumentException>(() => _sut.CalculateExecutionPlan(request));
        Assert.Equal(nameof(request.Amount), exception.ParamName);
    }
    
    
    [Theory]
    [InlineData(null, 1)]
    [InlineData("", 1)]
    [InlineData("a", 1)]
    [InlineData("buy1", 1)]
    public void ShouldThrowExceptionIfOrderTypeIsNonBuyOrSell(string orderType, decimal amount)
    {
        //setup
        var request = new ExecutionRequest()
        {
            OrderType = orderType,
            Amount = amount
        };
        
        // Act & Assert
        var exception = Assert.Throws<ArgumentException>(() => _sut.CalculateExecutionPlan(request));
        Assert.Equal(nameof(request.OrderType), exception.ParamName);
    }


  
    
    [Theory]
    [InlineData("buy")]
    [InlineData("sell")]
    public void ShouldReturnNullIfBookOrderIsEmpty(string orderType)
    {
        //setup
        var mockCryptoExchangeRepository = new Mock<ICryptoExchangeRepository>();
        mockCryptoExchangeRepository.Setup(x => x.GetCryptoExchangeData()).Returns([]);
        var cryptoExchangeDataProvider = new CryptoExchangeDataProvider(mockCryptoExchangeRepository.Object);
        var exchangeService = new MetaExchangeService(cryptoExchangeDataProvider, new NullLoggerFactory());
        var request = new ExecutionRequest
        {
            OrderType = orderType,
            Amount = 1
        };

        var exception = Assert.Throws<ApplicationException>(() => exchangeService.CalculateExecutionPlan(request));
        Assert.Equal("Book order is empty.", exception.Message);
    }
    
    
    [Theory]
    [InlineData("buy", 0)]
    [InlineData("buy", 1)]
    [InlineData("buy", -1)]
    [InlineData("sell", 0)]
    [InlineData("sell", 1)]
    [InlineData("sell", -1)]
    public void ShouldReturnNullIfCryptoBalanceIsLessThenRequestAmount(string orderType, decimal balance)
    {
        //setup
        var request = new ExecutionRequest
        {
            OrderType = orderType,
            Amount = 10000 //must be more than balance 
        };
        var exchanges = new List<CryptoExchange>
        {
            new CryptoExchange
            {
                ExchangeId = 1,
                Balance = new CryptoExchangeBalance
                {
                    Eur = balance,
                    Btc = balance
                },
                BookOrder = _bookOrders1
            }
        };
        
        var mockCryptoExchangeRepository = new Mock<ICryptoExchangeRepository>();
        mockCryptoExchangeRepository.Setup(x => x.GetCryptoExchangeData()).Returns(exchanges);
        var cryptoExchangeDataProvider = new CryptoExchangeDataProvider(mockCryptoExchangeRepository.Object);
        var exchangeService = new MetaExchangeService(cryptoExchangeDataProvider, new NullLoggerFactory());
        
        var exception = Assert.Throws<ApplicationException>(() => exchangeService.CalculateExecutionPlan(request));
        Assert.Equal("Crypto exchanges are out of balance.", exception.Message);
    }

    
    [Theory]
    [MemberData(nameof(TestResult))]
    public void ShouldCalculateTheExpectedPlan(ExecutionRequest request, List<ExecutionResponse> expected)
    {
        var mockCryptoExchangeRepository = new Mock<ICryptoExchangeRepository>();
        mockCryptoExchangeRepository.Setup(x => x.GetCryptoExchangeData()).Returns(TestData);
        var cryptoExchangeDataProvider = new CryptoExchangeDataProvider(mockCryptoExchangeRepository.Object);
        var exchangeService = new MetaExchangeService(cryptoExchangeDataProvider, new NullLoggerFactory());

        var res = exchangeService.CalculateExecutionPlan(request);
        
        Assert.Equivalent(expected, res);
        
        //sum should be equal to requested amount
        var sum = res.Sum(x => x.Amount);
        Assert.Equal(request.Amount, sum);
    }
    
    
    private static List<CryptoExchange> TestData =>
    [
        new()
        {
            ExchangeId = 1,
            Balance = new CryptoExchangeBalance
            {
                Eur = 10,
                Btc = 10
            },
            BookOrder = new BookOrder
            {
                Asks =
                [
                    new()
                    {
                        Order = Order1 with
                        {
                            ExchangeId = 1
                        }
                    },

                    new()
                    {
                        Order = Order1 with
                        {
                            ExchangeId = 1,
                        }
                    }
                ],

                Bids = new List<Bid>
                {
                    new()
                    {
                        Order = Order1 with
                        {
                            ExchangeId = 1
                        }
                    },
                    new()
                    {
                        Order = Order2 with
                        {
                            ExchangeId = 1
                        }
                    }
                }
            }
        },
        new()
        {
            ExchangeId = 2,
            Balance = new CryptoExchangeBalance
            {
                Eur = 5,
                Btc = 5
            },
            BookOrder = new BookOrder
            {
                Asks = new List<Ask>
                {
                    new()
                    {
                        Order = Order3 with
                        {
                            ExchangeId = 2
                        }
                    }
                },

                Bids = new List<Bid>
                {
                    new()
                    {
                        Order = Order3 with
                        {
                            ExchangeId = 2
                        }
                    },
                }
            },
        }
    ];

    public static TheoryData<ExecutionRequest, List<ExecutionResponse>> TestResult =>
        new()
        {
            {
                new ExecutionRequest
                {
                    Amount = 4,
                    OrderType = "buy"
                },
                new List<ExecutionResponse>
                {
                    new ExecutionResponse()
                    {
                        ExchangeId = 2,
                        OrderType = "buy",
                        Amount = Order3.Amount,
                        Price = Order3.Price
                    },
                    new ExecutionResponse()
                    {
                        ExchangeId = 1,
                        OrderType = "buy",
                        Amount = 3, // only as much as it is requested
                        Price = Order1.Price
                    }
                }
            },
            {
                new ExecutionRequest()
                {
                    Amount = 4,
                    OrderType = "sell"
                },
                new List<ExecutionResponse>
                {
                    new ExecutionResponse()
                    {
                        ExchangeId = 1,
                        OrderType = "sell",
                        Amount = Order2.Amount,
                        Price = Order2.Price
                    },
                    new ExecutionResponse()
                    {
                        ExchangeId = 1,
                        OrderType = "sell",
                        Amount = 1, // only as much as it is requested
                        Price = Order1.Price
                    }
                }
            }
        };

}