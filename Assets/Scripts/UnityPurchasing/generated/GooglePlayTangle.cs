// WARNING: Do not modify! Generated file.

namespace UnityEngine.Purchasing.Security {
    public class GooglePlayTangle
    {
        private static byte[] data = System.Convert.FromBase64String("0BPDtgDOgw+jKKyVq1JBcsQEIGfPQb2E1GWHHOi6+b715dZtg9qsDlxqgSdCHna6xzfj6bRDSDcfx93uIAXP5I9IFbhCN+6+FzTqbOZ0qsVwNbvjGnyVgG8QHiE5auDv5Y/jOSM8DOdm71+oOw1Au8AKyMlWPOUNQfNwU0F8d3hb9zn3hnxwcHB0cXJdLm9uEaQ4hUfZDmyo8898BLNDrfNwfnFB83B7c/NwcHH/QZpFwqrBOfC6DI26Ssn8MFaNf/ls+IZQddJz/uDigmO8Wp+zvkoUfaMbCoPXeU22H1HDc3Sgg7axFRjZpMR9966YlFSO4m7qh9SD3I5CXzZswB6oNyTdEFWpH6RpDvxuF/z3yx7+hEwQHqOr+GX1iJp0TnNycHFw");
        private static int[] order = new int[] { 9,3,11,10,13,11,9,7,10,10,12,13,12,13,14 };
        private static int key = 113;

        public static readonly bool IsPopulated = true;

        public static byte[] Data() {
        	if (IsPopulated == false)
        		return null;
            return Obfuscator.DeObfuscate(data, order, key);
        }
    }
}
