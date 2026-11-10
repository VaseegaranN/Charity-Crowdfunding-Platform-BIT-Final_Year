using Microsoft.AspNetCore.Identity;
namespace UnityFunds.Web.Services;
/// <summary>Hashes new passwords and transparently upgrades legacy plaintext values at successful login.</summary>
public sealed class PasswordService {
 private readonly PasswordHasher<string> _hasher = new();
 public string Hash(string password) => _hasher.HashPassword("UnityFunds", password);
 public bool VerifyAndUpgrade(string stored, string supplied, out string upgraded) {
  upgraded = stored;
  if (string.IsNullOrEmpty(stored) || string.IsNullOrEmpty(supplied)) return false;
  if (stored.StartsWith("AQAAAA", StringComparison.Ordinal)) {
   var result = _hasher.VerifyHashedPassword("UnityFunds", stored, supplied);
   if (result == PasswordVerificationResult.SuccessRehashNeeded) upgraded = Hash(supplied);
   return result != PasswordVerificationResult.Failed;
  }
  // Legacy accounts are upgraded on first successful sign-in.
  if (!string.Equals(stored, supplied, StringComparison.Ordinal)) return false;
  upgraded = Hash(supplied); return true;
 }
}
