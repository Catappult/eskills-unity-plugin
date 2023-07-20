package com.appcoins.eskills_purchase

import android.app.Activity
import android.content.Intent
import android.content.pm.PackageManager
import android.net.Uri
import android.os.Bundle
import android.util.Log
import com.unity3d.player.UnityPlayer


class PurchaseActivity : Activity() {
    companion object {
        private const val VALUE_KEY = "VALUE"
        private const val CURRENCY_KEY = "CURRENCY"
        private const val PRODUCT_KEY = "PRODUCT"
        private const val TIMEOUT_KEY = "TIMEOUT"
        private const val USER_NAME_KEY = "USER_NAME"
        private const val USER_ID_KEY = "USER_ID"
        private const val ENVIRONMENT_KEY = "ENVIRONMENT"
        private const val NUMBER_OF_PLAYERS = "NUMBER_OF_PLAYERS"
        private const val SESSION_KEY = "SESSION"

        private val TAG = PurchaseActivity::class.java.simpleName

        public @JvmStatic
        fun start(
                context: Activity, userName: String, value: Float, currency: String,
                product: String, timeout: Int, userId: String, environment: String,
                numberOfPlayers: Int
        ) {
            android.util.Log.d(TAG, "start: $context")
            val starter = Intent(context, PurchaseActivity::class.java)
            starter.putExtra(USER_NAME_KEY, userName)
            starter.putExtra(VALUE_KEY, value)
            starter.putExtra(CURRENCY_KEY, currency)
            starter.putExtra(PRODUCT_KEY, product)
            starter.putExtra(TIMEOUT_KEY, timeout)
            starter.putExtra(USER_ID_KEY, userId)
            starter.putExtra(ENVIRONMENT_KEY, environment)
            starter.putExtra(NUMBER_OF_PLAYERS, numberOfPlayers)
            context.startActivity(starter)
        }

        private const val REQUEST_CODE = 1234
    }

      override fun onCreate(savedInstanceState: Bundle?) {
        super.onCreate(savedInstanceState)
        val value = intent.getFloatExtra(VALUE_KEY, 1f)
        val currency = intent.getStringExtra(CURRENCY_KEY)
        val product = intent.getStringExtra(PRODUCT_KEY)
        val timeout = intent.getIntExtra(TIMEOUT_KEY, 3600)
        val numberOfPlayers = intent.getIntExtra(NUMBER_OF_PLAYERS, 2)
        val userName = intent.getStringExtra(USER_NAME_KEY)
        val userId = intent.getStringExtra(USER_ID_KEY)
        val environment = intent.getStringExtra(ENVIRONMENT_KEY)
        val intent =
            buildTargetIntent("https://apichain.catappult.io/transaction/eskills?" +
                "value=$value&currency=$currency&product=$product&" +
                "user_name=$userName&user_id=$userId&domain=${applicationContext.packageName}" +
                "&environment=$environment&number_of_users=$numberOfPlayers&timeout=$timeout")
        startActivityForResult(intent, REQUEST_CODE)
      }

    override fun onActivityResult(requestCode: Int, resultCode: Int, data: Intent?) {
        super.onActivityResult(requestCode, resultCode, data)
        if (requestCode == REQUEST_CODE) {
            var session = data?.getStringExtra(SESSION_KEY)
            if (session == null) {
                session = ""
            }
            Log.d(TAG, "$session")
            UnityPlayer.UnitySendMessage(
                    "Eskills",
                    "OnMatchCreated",
                    session
            )
            finish()
        }
    }

    private fun buildTargetIntent(url: String): Intent {
        val intent = Intent(Intent.ACTION_VIEW)
        intent.data = Uri.parse(url)

        // Check if there is an application that can process the AppCoins Billing
        // flow
        val packageManager: PackageManager = getApplicationContext().getPackageManager()
        val appsList = packageManager
                .queryIntentActivities(intent, PackageManager.MATCH_DEFAULT_ONLY)
        for (app in appsList) {
            if (app.activityInfo.packageName == "cm.aptoide.pt") {
                // If there's aptoide installed always choose Aptoide as default to open
                // url
                intent.setPackage(app.activityInfo.packageName)
                break
            } else if (app.activityInfo.packageName == "com.appcoins.wallet") {
                // If Aptoide is not installed and wallet is installed then choose Wallet
                // as default to open url
                intent.setPackage(app.activityInfo.packageName)
            }
        }
        return intent
    }

}