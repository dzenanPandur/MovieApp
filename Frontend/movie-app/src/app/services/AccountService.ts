class AccountService {
  async getAccessToken(): Promise<string | null> {
    return localStorage.getItem("access_token");
  }
}

const accountService = new AccountService();
export default accountService;  